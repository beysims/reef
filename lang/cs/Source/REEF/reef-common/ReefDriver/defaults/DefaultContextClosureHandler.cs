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

using Org.Apache.Reef.Common.Context;
using Org.Apache.Reef.Driver.Context;
using Org.Apache.Reef.Utilities.Logging;
using Org.Apache.Reef.Tang.Annotations;
using System;

namespace Org.Apache.Reef.Driver.Defaults
{
    /// <summary>
    /// Default event handler for ClosedContext: Logging it.
    /// </summary>
    public class DefaultContextClosureHandler : IObserver<IClosedContext>
    {
        private static readonly Logger LOGGER = Logger.GetLogger(typeof(DefaultContextClosureHandler));
        
        [Inject]
        public DefaultContextClosureHandler()
        {
        }

        public void OnNext(IClosedContext value)
        {
            LOGGER.Log(Level.Info, "Received ClosedContext :" + value.Id);
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
