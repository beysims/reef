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

using Org.Apache.Reef.Common.io;
using Org.Apache.Reef.IO.Network.Naming.Events;
using Org.Apache.Reef.Utilities;
using Org.Apache.Reef.Wake.Remote;
using System.Collections.Generic;
using System.Linq;
using org.apache.reef.io.network.naming.avro;

namespace Org.Apache.Reef.IO.Network.Naming.Codec
{
    internal class NamingLookupResponseCodec : ICodec<NamingLookupResponse>
    {
        public byte[] Encode(NamingLookupResponse obj)
        {
            List<AvroNamingAssignment> tuples = obj.NameAssignments
                .Select(assignment => new AvroNamingAssignment()
                {
                    id = assignment.Identifier, 
                    host = assignment.Endpoint.Address.ToString(),
                    port = assignment.Endpoint.Port
                }).ToList();

            AvroNamingLookupResponse response = new AvroNamingLookupResponse { tuples = tuples };
            return AvroUtils.AvroSerialize(response);
        }

        public NamingLookupResponse Decode(byte[] data)
        {
            AvroNamingLookupResponse response = AvroUtils.AvroDeserialize<AvroNamingLookupResponse>(data);
            List<NameAssignment> assignments =
                response.tuples.Select(x => new NameAssignment(x.id, x.host, x.port)).ToList();

            return new NamingLookupResponse(assignments);
        }
    }
}
