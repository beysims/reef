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
using Org.Apache.Reef.Tang.Annotations;
using Org.Apache.Reef.Tang.Exceptions;
using Org.Apache.Reef.Tang.Implementations;
using Org.Apache.Reef.Tang.Interface;
using Org.Apache.Reef.Tang.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Org.Apache.Reef.Tang.Test.Injection
{
    public interface IH
    {
    }

    [TestClass]
    public class TestMultipleConstructors
    {
        [TestMethod]
        public void TestMissingAllParameters()
        {
            //Multiple infeasible plans: Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //, Org.Apache.Reef.Tang.Test
            //, Version=1.0.0.0
            //, Culture=neutral
            //, PublicKeyToken=null:

            //  [ Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest = new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.Boolean Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedBool = 
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 
            //    ) 
            //    | new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.String Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedString = 
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 
            //    ) 
            //    | new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.Boolean Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedBool = 
            //    , System.String Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedString = 
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 
            //    )
            //  ]
            MultiConstructorTest obj = null;
            try
            {
                ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
                IInjector i = TangFactory.GetTang().NewInjector(cb.Build());
                obj = i.GetInstance<MultiConstructorTest>();
            }
            catch (InjectionException)
            {               
            }
            Assert.IsNull(obj);
        }

        [TestMethod]
        public void TestMissingIntParameter()
        {
            //Multiple infeasible plans: Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //, Org.Apache.Reef.Tang.Test
            //, Version=1.0.0.0
            //, Culture=neutral
            //, PublicKeyToken=null:
            //  [ Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest = new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.Boolean Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedBool = False
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 
            //    ) 
            //    | new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.String Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedString = foo
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 
            //    ) 
            //    | new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.Boolean Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedBool = False
            //    , System.String Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedString = foo
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 
            //    )
            //  ]
            MultiConstructorTest obj = null;
            try
            {
                ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
                cb.BindNamedParameter<MultiConstructorTest.NamedString, string>(GenericType<MultiConstructorTest.NamedString>.Class, "foo");
                cb.BindNamedParameter<MultiConstructorTest.NamedBool, bool>(GenericType<MultiConstructorTest.NamedBool>.Class, "true");
                IInjector i = TangFactory.GetTang().NewInjector(cb.Build());
                obj = i.GetInstance<MultiConstructorTest>();
            }
            catch (InjectionException)
            {
            }
            Assert.IsNull(obj);
        }

        [TestMethod]
        public void TestOnlyBoolParameter()
        {
            //Multiple infeasible plans: Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //, Org.Apache.Reef.Tang.Test
            //, Version=1.0.0.0
            //, Culture=neutral
            //, PublicKeyToken=null:
            //  [ Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest = new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.Boolean Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedBool = False
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 
            //    ) 
            //    | new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.String Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedString = 
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 
            //    ) 
            //    | new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.Boolean Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedBool = False
            //    , System.String Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedString = 
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 
            //    )
            //  ]
            MultiConstructorTest obj = null;
            try
            {
                ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
                cb.BindNamedParameter<MultiConstructorTest.NamedBool, bool>(GenericType<MultiConstructorTest.NamedBool>.Class, "true");
                IInjector i = TangFactory.GetTang().NewInjector(cb.Build());
                obj = i.GetInstance<MultiConstructorTest>();
            }
            catch (InjectionException)
            {
            }
            Assert.IsNull(obj);
        }

        [TestMethod]
        public void TestOnlyIntParameter()
        {
            //Multiple infeasible plans: Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //, Org.Apache.Reef.Tang.Test
            //, Version=1.0.0.0
            //, Culture=neutral
            //, PublicKeyToken=null:
            //  [ Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest = new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.Boolean Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedBool = 
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 8
            //    ) 
            //    | new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.String Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedString = 
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 8
            //    ) 
            //    | new Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest
            //    ( System.Boolean Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedBool = 
            //    , System.String Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedString = 
            //    , System.Int32 Org.Apache.Reef.Tang.Test.Injection.MultiConstructorTest+NamedInt = 8
            //    )
            //  ]
            MultiConstructorTest obj = null;
            try
            {
                ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
                cb.BindNamedParameter<MultiConstructorTest.NamedInt, int>(GenericType<MultiConstructorTest.NamedInt>.Class, "8");
                IInjector i = TangFactory.GetTang().NewInjector(cb.Build());
                obj = i.GetInstance<MultiConstructorTest>();
            }
            catch (InjectionException)
            {
            }
            Assert.IsNull(obj);
        }

        [TestMethod]
        public void TestMultipleConstructor()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
            cb.BindNamedParameter<MultiConstructorTest.NamedString, string>(GenericType<MultiConstructorTest.NamedString>.Class, "foo");
            cb.BindNamedParameter<MultiConstructorTest.NamedInt, int>(GenericType<MultiConstructorTest.NamedInt>.Class, "8");
            cb.BindNamedParameter<MultiConstructorTest.NamedBool, bool>(GenericType<MultiConstructorTest.NamedBool>.Class, "true");
            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());
            var o = i.GetInstance<MultiConstructorTest>();
            o.Verify("foo", 8, true);
        }

        [TestMethod]
        public void TestMultiLayersWithMiddleLayerFirst()
        {
            TangImpl.Reset();
            ICsConfigurationBuilder cb2 = TangFactory.GetTang().NewConfigurationBuilder();
            cb2.BindImplementation(typeof(IH), typeof(M));
            IInjector i2 = TangFactory.GetTang().NewInjector(cb2.Build());
            var o2 = i2.GetInstance<IH>();
            Assert.IsTrue(o2 is M);

            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
            cb.BindImplementation(typeof(IH), typeof(L));
            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());
            var o = i.GetInstance<IH>();
            Assert.IsTrue(o is L);           
        }

        [TestMethod]
        public void TestMultiLayersWithLowerLayerFirst()
        {
            TangImpl.Reset(); 
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
            cb.BindImplementation(typeof(IH), typeof(L));
            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());
            var o = i.GetInstance<IH>();
            Assert.IsTrue(o is L);

            ICsConfigurationBuilder cb2 = TangFactory.GetTang().NewConfigurationBuilder();
            cb2.BindImplementation(typeof(IH), typeof(M));                              
            IInjector i2 = TangFactory.GetTang().NewInjector(cb2.Build());
            var o2 = i2.GetInstance<IH>();
            Assert.IsTrue(o2 is M);
        }

        [TestMethod]
        public void TestMultiLayersWithBindImpl()
        {
            TangImpl.Reset(); 

            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
            cb.BindImplementation(typeof(IH), typeof(L));
            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());
            var o = i.GetInstance<IH>();
            Assert.IsTrue(o is L);

            ICsConfigurationBuilder cb2 = TangFactory.GetTang().NewConfigurationBuilder();
            cb2.BindImplementation(typeof(IH), typeof(M));
            cb2.BindImplementation(typeof(M), typeof(L)); //construcotr of L is explicitly bound
            IInjector i2 = TangFactory.GetTang().NewInjector(cb2.Build());
            var o2 = i2.GetInstance<IH>();
            Assert.IsTrue(o2 is L);
        }

        [TestMethod]
        public void TestMultiLayersWithNoInjectableDefaultConstructor()
        {
            TangImpl.Reset(); 

            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
            cb.BindImplementation(typeof(IH), typeof(L1));
            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());
            var o = i.GetInstance<IH>();
            Assert.IsNotNull(o);

            ICsConfigurationBuilder cb2 = TangFactory.GetTang().NewConfigurationBuilder();
            cb2.BindImplementation(typeof(IH), typeof(M1));  //M1 doesn't have injectable default constructor, no implementation L1 is bound to M1
            IInjector i2 = TangFactory.GetTang().NewInjector(cb2.Build());
            string msg = null;
            try
            {
                var o2 = i2.GetInstance<IH>();
                Assert.IsTrue(o2 is L1);
            }
            catch (Exception e)
            {
                //Cannot inject Org.Apache.Reef.Tang.Test.Injection.IH, Org.Apache.Reef.Tang.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null: 
                //No known implementations / injectable constructors for Org.Apache.Reef.Tang.Test.Injection.M1, Org.Apache.Reef.Tang.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
                msg = e.Message;
            }
            Assert.IsNotNull(msg);
        }
    }

    public class MultiConstructorTest
    {
        private readonly string str;
        private readonly int iVal;
        private readonly bool bVal;

        [Inject]
        public MultiConstructorTest([Parameter(typeof(NamedBool))] bool b, [Parameter(typeof(NamedInt))] int i)
        {
            this.bVal = b;
            this.iVal = i;
            this.bVal = false;
        }

        [Inject]
        public MultiConstructorTest([Parameter(typeof(NamedString))] string s, [Parameter(typeof(NamedInt))] int i)
        {
            this.str = s;
            this.iVal = i;
            this.bVal = false;
        }

        [Inject]
        public MultiConstructorTest([Parameter(typeof(NamedBool))] bool b, [Parameter(typeof(NamedString))] string s, [Parameter(typeof(NamedInt))] int i)
        {
            this.str = s;
            this.iVal = i;
            this.bVal = b;
        }

        public void Verify(string s, int i, bool b)
        {
            Assert.AreEqual(str, s);
            Assert.AreEqual(iVal, i);
            Assert.AreEqual(bVal, b);
        }

        [NamedParameter]
        public class NamedString : Name<string>
        {
        }

        [NamedParameter]
        public class NamedInt : Name<int>
        {
        }

        [NamedParameter]
        public class NamedBool : Name<bool>
        {
        }
    }

    class M : IH
    {
        [Inject]
        public M()
        {            
        }
    }

    class L : M
    {
        [Inject]
        public L()
        {            
        }
    }

    class M1 : IH
    {
        public M1()
        {            
        }
    }

    class L1 : M1
    {
        [Inject]
        public L1()
        {            
        }
    }
}