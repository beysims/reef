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

using ProtoBuf;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.Apache.Reef.Wake.Util
{
    public class SerializationHelper
    {
        public static byte[] Serialize<T>(T t)
        {
            using (var s = new MemoryStream())
            {
                Serializer.Serialize(s, t);
                return s.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            using (var s = new MemoryStream(bytes))
            {
                return Serializer.Deserialize<T>(s);
            }
        }
    }
}
