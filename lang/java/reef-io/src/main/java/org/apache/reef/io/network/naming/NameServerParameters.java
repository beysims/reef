/**
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
package org.apache.reef.io.network.naming;

import org.apache.reef.io.network.util.StringIdentifierFactory;
import org.apache.reef.tang.annotations.Name;
import org.apache.reef.tang.annotations.NamedParameter;
import org.apache.reef.wake.IdentifierFactory;

public class NameServerParameters {

  @NamedParameter(doc = "port for the name service", default_value = "0", short_name = "nameport")
  public class NameServerPort implements Name<Integer> {
  }

  @NamedParameter(doc = "DNS hostname running the name service")
  public class NameServerAddr implements Name<String> {
  }

  @NamedParameter(doc = "identifier factory for the name service", default_class = StringIdentifierFactory.class)
  public class NameServerIdentifierFactory implements Name<IdentifierFactory> {
  }

}
