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

using Org.Apache.Reef.Driver.Bridge;
using Org.Apache.Reef.Driver.Evaluator;
using Org.Apache.Reef.Tang.Annotations;
using System;

namespace Org.Apache.Reef.Examples.HelloCLRBridge
{
    public class HelloFailedEvaluatorHandler : IObserver<IFailedEvaluator>
    {
        private static int _failureCount = 0;

        private static int _maxTrial = 2;

        [Inject]
        public HelloFailedEvaluatorHandler()
        {
        }

        public void OnNext(IFailedEvaluator failedEvaluator)
        {
            Console.WriteLine("Receive a failed evaluator: " + failedEvaluator.Id);
            if (++_failureCount < _maxTrial)
            {
                Console.WriteLine("Requesting another evaluator");
                EvaluatorRequest newRequest = new EvaluatorRequest(1, 512, "somerack");
                IEvaluatorRequestor requestor = failedEvaluator.GetEvaluatorRequetor();
                if (failedEvaluator.GetEvaluatorRequetor() != null)
                {
                    requestor.Submit(newRequest);
                }
            }
            else
            {
                Console.WriteLine("Exceed max retries number");
                throw new Exception("Unrecoverable evaluator failure.");
            }
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