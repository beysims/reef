﻿/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Org.Apache.Reef.Driver.bridge;
using Org.Apache.Reef.Utilities.Logging;
using Org.Apache.Reef.Tang.Formats;
using Org.Apache.Reef.Tang.Implementations.Configuration;
using Org.Apache.Reef.Tang.Interface;
using Org.Apache.Reef.Tang.Protobuf;

namespace Org.Apache.Reef.Driver
{
    public class DriverConfigGenerator
    {
        public const string DriverConfigFile = "driver.config";
        public const string JobDriverConfigFile = "jobDriver.config";
        public const string DriverChFile = "driverClassHierarchy.bin";
        public const string HttpServerConfigFile = "httpServer.config";
        public const string NameServerConfigFile = "nameServer.config";
        public const string UserSuppliedGlobalLibraries = "userSuppliedGlobalLibraries.txt";

        private static readonly Logger Log = Logger.GetLogger(typeof(DriverConfigGenerator));

        public static void DriverConfigurationBuilder(DriverConfigurationSettings driverConfigurationSettings)
        {
            ExtractConfigFromJar(driverConfigurationSettings.JarFileFolder);

            if (!File.Exists(DriverChFile))
            {
                Log.Log(Level.Warning, string.Format(CultureInfo.CurrentCulture, "There is no file {0} extracted from the jar file at {1}.", DriverChFile, driverConfigurationSettings.JarFileFolder));
                return;
            }

            if (!File.Exists(HttpServerConfigFile))
            {
                Log.Log(Level.Warning, string.Format(CultureInfo.CurrentCulture, "There is no file {0} extracted from the jar file at {1}.", HttpServerConfigFile, driverConfigurationSettings.JarFileFolder));
                return;
            }

            if (!File.Exists(JobDriverConfigFile))
            {
                Log.Log(Level.Warning, string.Format(CultureInfo.CurrentCulture, "There is no file {0} extracted from the jar file at {1}.", JobDriverConfigFile, driverConfigurationSettings.JarFileFolder));
                return;
            }

            if (!File.Exists(NameServerConfigFile))
            {
                Log.Log(Level.Warning, string.Format(CultureInfo.CurrentCulture, "There is no file {0} extracted from the jar file at {1}.", NameServerConfigFile, driverConfigurationSettings.JarFileFolder));
                return;
            }

            AvroConfigurationSerializer serializer = new AvroConfigurationSerializer();

            IClassHierarchy drvierClassHierarchy = ProtocolBufferClassHierarchy.DeSerialize(DriverChFile);

            AvroConfiguration jobDriverAvroconfiguration = serializer.AvroDeseriaizeFromFile(JobDriverConfigFile);
            IConfiguration jobDriverConfiguration = serializer.FromAvro(jobDriverAvroconfiguration, drvierClassHierarchy);

            AvroConfiguration httpAvroconfiguration = serializer.AvroDeseriaizeFromFile(HttpServerConfigFile);
            IConfiguration httpConfiguration = serializer.FromAvro(httpAvroconfiguration, drvierClassHierarchy);

            AvroConfiguration nameAvroconfiguration = serializer.AvroDeseriaizeFromFile(NameServerConfigFile);
            IConfiguration nameConfiguration = serializer.FromAvro(nameAvroconfiguration, drvierClassHierarchy);

            IConfiguration merged;

            if (driverConfigurationSettings.IncludingHttpServer && driverConfigurationSettings.IncludingNameServer)
            {
                merged = Configurations.MergeDeserializedConfs(jobDriverConfiguration, httpConfiguration, nameConfiguration);
            } 
            else if (driverConfigurationSettings.IncludingHttpServer)
            {
                merged = Configurations.MergeDeserializedConfs(jobDriverConfiguration, httpConfiguration);                
            }
            else if (driverConfigurationSettings.IncludingNameServer)
            {
                merged = Configurations.MergeDeserializedConfs(jobDriverConfiguration, nameConfiguration);
            }
            else
            {
                merged = jobDriverConfiguration;
            }

            var b = merged.newBuilder();

            b.BindSetEntry("org.apache.reef.driver.parameters.DriverIdentifier", driverConfigurationSettings.DriverIdentifier);
            b.Bind("org.apache.reef.driver.parameters.DriverMemory", driverConfigurationSettings.DriverMemory.ToString(CultureInfo.CurrentCulture));
            b.Bind("org.apache.reef.driver.parameters.DriverJobSubmissionDirectory", driverConfigurationSettings.SubmissionDirectory);

            //add for all the globallibaries
            if (File.Exists(UserSuppliedGlobalLibraries))
            {
                var globalLibString = File.ReadAllText(UserSuppliedGlobalLibraries);
                if (!string.IsNullOrEmpty(globalLibString))
                {
                    foreach (string fname in globalLibString.Split(','))
                    {
                        b.BindSetEntry("org.apache.reef.driver.parameters.JobGlobalLibraries", fname);
                    }
                }
            }

            foreach (string f in Directory.GetFiles(driverConfigurationSettings.ClrFolder))
            {
                b.BindSetEntry("org.apache.reef.driver.parameters.JobGlobalFiles", f);
            }

            IConfiguration c = b.Build();

            serializer.ToFile(c, DriverConfigFile);

            Log.Log(Level.Info, string.Format(CultureInfo.CurrentCulture, "driver.config is written to: {0} {1}.", Directory.GetCurrentDirectory(), DriverConfigFile));

            //additional file for easy to read
            using (StreamWriter outfile = new StreamWriter(DriverConfigFile + ".txt"))
            {
                outfile.Write(serializer.ToString(c));
            }
        }

        private static void ExtractConfigFromJar(string jarfileFolder)
        {
            string jarfile = jarfileFolder + Constants.BridgeJarFileName;
            List<string> files = new List<string>();
            files.Add(DriverConfigGenerator.HttpServerConfigFile);
            files.Add(DriverConfigGenerator.JobDriverConfigFile);
            files.Add(DriverConfigGenerator.NameServerConfigFile);
            files.Add(DriverConfigGenerator.DriverChFile);
            ClrClientHelper.ExtractConfigfileFromJar(jarfile, files, ".");
        }
    }
}
