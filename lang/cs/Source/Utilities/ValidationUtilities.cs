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

using System;
using System.Globalization;
using Org.Apache.Reef.Utilities.Logging;

namespace Org.Apache.Reef.Utilities
{
    public class ValidationUtilities
    {
        private static readonly Logger LOGGER = Logger.GetLogger(typeof(ValidationUtilities));

        public static string ValidateEnvVariable(string env)
        {
            string envVariable = Environment.GetEnvironmentVariable(env);
            if (string.IsNullOrWhiteSpace(envVariable))
            {
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} not set. Please set the environment variable first. Exiting...", env));
                string msg = string.Format(CultureInfo.InvariantCulture, "No {0} found.", env);
                Diagnostics.Exceptions.Throw(new InvalidOperationException(msg), msg, LOGGER);
            }
            return envVariable;
        }
    }
}
