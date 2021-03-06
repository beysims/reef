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
using System.Text;
using Org.Apache.Reef.Driver.Task;
using Org.Apache.Reef.Tang.Annotations;

namespace Org.Apache.Reef.Examples.HelloCLRBridge
{
    public class HelloTaskMessageHandler : IObserver<ITaskMessage>
    {
        [Inject]
        public HelloTaskMessageHandler()
        {
        }

        public void OnNext(ITaskMessage taskMessage)
        {
            Console.WriteLine(string.Format(
                CultureInfo.InvariantCulture,
                "CLR HelloTaskMessageHandler received following message from Task: {0}, Message: {1}.",
                taskMessage.TaskId,
                Encoding.UTF8.GetString(taskMessage.Message)));           
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
    }
}