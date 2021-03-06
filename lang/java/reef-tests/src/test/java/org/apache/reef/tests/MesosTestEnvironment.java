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
package org.apache.reef.tests;

import org.apache.reef.runtime.mesos.client.MesosClientConfiguration;
import org.apache.reef.runtime.yarn.client.YarnClientConfiguration;
import org.apache.reef.tang.Configuration;
import org.apache.reef.tang.exceptions.BindException;

/**
 * A TestEnvironment for the Mesos resourcemanager.
 */
public final class MesosTestEnvironment extends TestEnvironmentBase implements TestEnvironment {

  // Used to make sure the tests call the methods in the right order.
  private boolean ready = false;

  @Override
  public synchronized final void setUp() {
    this.ready = true;
  }

  @Override
  public synchronized final Configuration getRuntimeConfiguration() {
    assert (this.ready);
    try {
      if (System.getenv("REEF_TEST_MESOS_MASTER_IP").equals("")) {
        throw new RuntimeException("REEF_TEST_MESOS_MASTER_IP unspecified");
      }

      final String masterIp = System.getenv("REEF_TEST_MESOS_MASTER_IP");
      return MesosClientConfiguration.CONF
          .set(MesosClientConfiguration.MASTER_IP, masterIp)
          .build();
    } catch (final BindException ex) {
      throw new RuntimeException(ex);
    }
  }

  @Override
  public synchronized final void tearDown() {
    assert (this.ready);
    this.ready = false;
  }

  @Override
  public int getTestTimeout() {
    return 300000; // 5 minutes
  }


}
