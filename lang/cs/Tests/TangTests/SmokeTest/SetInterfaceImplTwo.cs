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

using Org.Apache.Reef.Tang.Annotations;

namespace Org.Apache.Reef.Tang.Test.SmokeTest
{
    public class SetInterfaceImplTwo : ISetInterface
    {
        private readonly double magicNumber;

        [Inject]
        SetInterfaceImplTwo() 
        {
            this.magicNumber = 42.0;
        }

        public void AMethod() 
        {
        }

        public override bool Equals(object o) 
        {
            if (this == o)
            {
                return true;
            }

            if (o == null || !(o is SetInterfaceImplTwo))
            {
                return false;
            }

            SetInterfaceImplTwo that = (SetInterfaceImplTwo)o;

            if (that.magicNumber.CompareTo(magicNumber) != 0)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return magicNumber.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (this == obj)
            {
                return 0;
            }

            if (obj == null || !(obj is SetInterfaceImplTwo))
            {
                return -1;
            }

            SetInterfaceImplTwo that = (SetInterfaceImplTwo)obj;

            return that.magicNumber.CompareTo(magicNumber);
        }
    }
}