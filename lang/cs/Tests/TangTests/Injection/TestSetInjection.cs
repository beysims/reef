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
using System.Collections.Generic;
using Org.Apache.Reef.Tang.Annotations;
using Org.Apache.Reef.Tang.Formats;
using Org.Apache.Reef.Tang.Implementations;
using Org.Apache.Reef.Tang.Interface;
using Org.Apache.Reef.Tang.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Org.Apache.Reef.Tang.Test.Injection
{
    public interface INumber : IComparable
    {
    }

    public interface ITimeshift
    {
        string LinkId { get; }

        TimeSpan TimeshiftSpan { get; }
    }

    [TestClass]
    public class TestSetInjection
    {
        [TestMethod]
        public void TestStringInjectDefault()
        {
            Box b = (Box)TangFactory.GetTang().NewInjector().GetInstance(typeof(Box));

            ISet<string> actual = b.Numbers;

            ISet<string> expected = new HashSet<string>();
            expected.Add("one");
            expected.Add("two");
            expected.Add("three");

            Assert.IsTrue(actual.Contains("one"));
            Assert.IsTrue(actual.Contains("two"));
            Assert.IsTrue(actual.Contains("three"));
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestObjectInjectDefault()
        {
            IInjector i = TangFactory.GetTang().NewInjector();
            i.BindVolatileInstance(GenericType<Integer>.Class, new Integer(42));
            i.BindVolatileInstance(GenericType<Float>.Class, new Float(42.0001f));
            ISet<INumber> actual = ((Pool)i.GetInstance(typeof(Pool))).Numbers;
            ISet<INumber> expected = new HashSet<INumber>();
            expected.Add(new Integer(42));
            expected.Add(new Float(42.0001f));

            Assert.IsTrue(actual.Contains(new Integer(42)));
            Assert.IsTrue(actual.Contains(new Float(42.0001f)));
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void testStringInjectBound()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
            cb.BindSetEntry<SetOfNumbers, string>(GenericType<SetOfNumbers>.Class, "four");
            cb.BindSetEntry<SetOfNumbers, string>(GenericType<SetOfNumbers>.Class, "five");
            cb.BindSetEntry<SetOfNumbers, string>(GenericType<SetOfNumbers>.Class, "six");

            Box b = (Box)TangFactory.GetTang().NewInjector(cb.Build()).GetInstance(typeof(Box));
            ISet<string> actual = b.Numbers;
            ISet<string> expected = new HashSet<string>();
            expected.Add("four");
            expected.Add("five");
            expected.Add("six");

            Assert.IsTrue(actual.Contains("four"));
            Assert.IsTrue(actual.Contains("five"));
            Assert.IsTrue(actual.Contains("six"));
        }

        [TestMethod]
        public void TestObjectInjectBound()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
            cb.BindSetEntry<SetOfClasses, Integer, INumber>(GenericType<SetOfClasses>.Class, GenericType<Integer>.Class);
            cb.BindSetEntry<SetOfClasses, Float, INumber>(GenericType<SetOfClasses>.Class, GenericType<Float>.Class);

            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());
            i.BindVolatileInstance(GenericType<Integer>.Class, new Integer(4));
            i.BindVolatileInstance(GenericType<Float>.Class, new Float(42.0001f));

            ISet<INumber> actual = i.GetInstance<Pool>().Numbers;
            ISet<INumber> expected = new HashSet<INumber>();
            expected.Add(new Integer(4));
            expected.Add(new Float(42.0001f));
            Assert.IsTrue(Utilities.Utilities.Equals<INumber>(actual, expected));
        }

        [TestMethod]
        public void TestSetOfClassBound()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
            cb.BindSetEntry<SetOfClasses, Integer1, INumber>(GenericType<SetOfClasses>.Class, GenericType<Integer1>.Class)  //bind an impl to the interface of the set
              .BindNamedParameter<Integer1.NamedInt, int>(GenericType<Integer1.NamedInt>.Class, "4"); //bind parameter for the impl

            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());

            ISet<INumber> actual = i.GetInstance<Pool>().Numbers;
            ISet<INumber> expected = new HashSet<INumber>();
            expected.Add(new Integer1(4));

            Assert.IsTrue(Utilities.Utilities.Equals<INumber>(actual, expected));
        }

        [TestMethod]
        public void TestSetOfClassWithDefault()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());

            ISet<INumber> actual = i.GetInstance<Pool1>().Numbers;
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void TestSetOfTimeshift()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();

            cb.BindSetEntry<SetOfTimeshifts, Timeshift, ITimeshift>(GenericType<SetOfTimeshifts>.Class, GenericType<Timeshift>.Class)
            .BindNamedParameter<Timeshift.TimeshiftLinkId, string>(GenericType<Timeshift.TimeshiftLinkId>.Class, "123")
            .BindNamedParameter<Timeshift.TimeshiftInTicks, long>(GenericType<Timeshift.TimeshiftInTicks>.Class, "10");

            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());

            ISet<ITimeshift> actual = i.GetInstance<SetofTimeShiftClass>().Timeshifts;
            Assert.IsTrue(actual.Count == 1);
        }

        [TestMethod]
        public void TestSetOfTimeshiftMultipleInstances()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();

            //when adding another Timeshift into the set for named parameter SetOfTimeshifts, it ends up the same entry. 
            cb.BindSetEntry<SetOfTimeshifts, Timeshift, ITimeshift>(GenericType<SetOfTimeshifts>.Class, GenericType<Timeshift>.Class);
            cb.BindSetEntry<SetOfTimeshifts, Timeshift, ITimeshift>(GenericType<SetOfTimeshifts>.Class, GenericType<Timeshift>.Class);
            cb.BindNamedParameter<Timeshift.TimeshiftLinkId, string>(GenericType<Timeshift.TimeshiftLinkId>.Class, "123")
            .BindNamedParameter<Timeshift.TimeshiftInTicks, long>(GenericType<Timeshift.TimeshiftInTicks>.Class, "10");

            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());

            ISet<ITimeshift> actual = i.GetInstance<SetofTimeShiftClass>().Timeshifts;
            Assert.IsTrue(actual.Count == 1);
        }

        [TestMethod]
        public void TestSetOfTimeshiftMultipleSubClasses()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();

            //Adding implementations from different subclasses
            cb.BindSetEntry<SetOfTimeshifts, Timeshift, ITimeshift>(GenericType<SetOfTimeshifts>.Class, GenericType<Timeshift>.Class);
            cb.BindSetEntry<SetOfTimeshifts, Timeshift1, ITimeshift>(GenericType<SetOfTimeshifts>.Class, GenericType<Timeshift1>.Class);

            cb.BindNamedParameter<Timeshift.TimeshiftLinkId, string>(GenericType<Timeshift.TimeshiftLinkId>.Class, "123")
            .BindNamedParameter<Timeshift.TimeshiftInTicks, long>(GenericType<Timeshift.TimeshiftInTicks>.Class, "10");

            cb.BindNamedParameter<Timeshift1.TimeshiftLinkId, string>(GenericType<Timeshift1.TimeshiftLinkId>.Class, "456")
            .BindNamedParameter<Timeshift1.TimeshiftInTicks, long>(GenericType<Timeshift1.TimeshiftInTicks>.Class, "20"); 

            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());

            ISet<ITimeshift> actual = i.GetInstance<SetofTimeShiftClass>().Timeshifts;
            Assert.IsTrue(actual.Count == 2);
        }

        [TestMethod]
        public void TestSetOfTimeshiftWithDefault()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();

            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());

            ISet<ITimeshift> actual = i.GetInstance<SetofTimeShiftClass>().Timeshifts;
            Assert.IsTrue(actual.Count == 1);
        }

        [TestMethod]
        public void TestSetOfTimeshiftWithEmptySet()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();

            IInjector i = TangFactory.GetTang().NewInjector(cb.Build());

            ISet<ITimeshift> actual = i.GetInstance<SetofTimeShiftClassWithoutDefault>().Timeshifts;
            Assert.IsTrue(actual.Count == 0);
        }        

        [TestMethod]
        public void TestObjectInjectRoundTrip()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
            cb.BindSetEntry<SetOfClasses, Integer, INumber>(GenericType<SetOfClasses>.Class, GenericType<Integer>.Class);
            cb.BindSetEntry<SetOfClasses, Float, INumber>(GenericType<SetOfClasses>.Class, GenericType<Float>.Class);

            AvroConfigurationSerializer serializer = new AvroConfigurationSerializer();
            IConfiguration c2 = serializer.FromString(serializer.ToString(cb.Build()));

            IInjector i = TangFactory.GetTang().NewInjector(c2);
            i.BindVolatileInstance(GenericType<Integer>.Class, new Integer(4));
            i.BindVolatileInstance(GenericType<Float>.Class, new Float(42.0001f));

            ISet<INumber> actual = i.GetInstance<Pool>().Numbers;
            ISet<INumber> expected = new HashSet<INumber>();
            expected.Add(new Integer(4));
            expected.Add(new Float(42.0001f));
            Assert.IsTrue(Utilities.Utilities.Equals<INumber>(actual, expected));
        }

        [TestMethod]
        public void TestStringInjectRoundTrip()
        {
            ICsConfigurationBuilder cb = TangFactory.GetTang().NewConfigurationBuilder();
            cb.BindSetEntry<SetOfNumbers, string>(GenericType<SetOfNumbers>.Class, "four");
            cb.BindSetEntry<SetOfNumbers, string>(GenericType<SetOfNumbers>.Class, "five");
            cb.BindSetEntry<SetOfNumbers, string>(GenericType<SetOfNumbers>.Class, "six");

            string s = ConfigurationFile.ToConfigurationString(cb.Build());
            ICsConfigurationBuilder cb2 = TangFactory.GetTang().NewConfigurationBuilder();
            ConfigurationFile.AddConfigurationFromString(cb2, s);

            ISet<string> actual =
                ((Box)TangFactory.GetTang().NewInjector(cb2.Build()).GetInstance(typeof(Box))).Numbers;

            Assert.IsTrue(actual.Contains("four"));
            Assert.IsTrue(actual.Contains("five"));
            Assert.IsTrue(actual.Contains("six"));
        }

        [TestMethod]
        public void TestDefaultAsClass()
        {
            IInjector i = TangFactory.GetTang().NewInjector();
            i.BindVolatileInstance(GenericType<Integer>.Class, new Integer(1));
            i.BindVolatileInstance(GenericType<Float>.Class, new Float(2f));
            ISet<INumber> actual =
                (ISet<INumber>)
                i.GetNamedInstance<SetOfClassesDefaultClass, ISet<INumber>>(GenericType<SetOfClassesDefaultClass>.Class);

            ISet<INumber> expected = new HashSet<INumber>();
            expected.Add(new Integer(1));
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.IsTrue(actual.Contains(new Integer(1)));
        }

        [TestMethod]
        public void TestInjectionExtension()
        {
            IInjector i = TangFactory.GetTang().NewInjector();
            i.BindVolatileInstance<Integer>(new Integer(1));
            i.BindVolatileInstance<Float>(new Float(2f));
            ISet<INumber> actual =
                (ISet<INumber>)
                i.GetNamedInstance<SetOfClassesDefaultClass, ISet<INumber>>();

            ISet<INumber> expected = new HashSet<INumber>();
            expected.Add(new Integer(1));
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.IsTrue(actual.Contains(new Integer(1)));
        }

        [NamedParameter(DefaultValues = new string[] { "one", "two", "three" })]
        public class SetOfNumbers : Name<ISet<string>>
        {
        }

        public class Box
        {
            [Inject]
            public Box([Parameter(typeof(SetOfNumbers))] ISet<string> numbers)
            {
                this.Numbers = numbers;
            }

            public ISet<string> Numbers { get; set; }
        }

        [NamedParameter(DefaultClasses = new Type[] { typeof(Integer), typeof(Float) })]
        public class SetOfClasses : Name<ISet<INumber>>
        {
        }

        public class Pool
        {
            [Inject]
            private Pool([Parameter(typeof(SetOfClasses))] ISet<INumber> numbers)
            {
                this.Numbers = numbers;
            }

            public ISet<INumber> Numbers { get; set; }
        }

        [NamedParameter(DefaultClass = typeof(Integer))]
        public class SetOfClassesDefaultClass : Name<ISet<INumber>>
        {
        }

        public class Integer : INumber
        {
            private int val;
          
            public Integer(int v)
            {
                val = v;
            }

            public int CompareTo(object obj)
            {
                if (!(obj is Integer))
                {
                    return -1;
                }
                if (this.val == ((Integer)obj).val)
                {
                    return 0;
                }

                if (this.val < ((Integer)obj).val)
                {
                    return -1;
                }

                return 1;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Integer))
                {
                    return false;
                }

                if (this.val == ((Integer)obj).val)
                {
                    return true;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return val.GetHashCode();
            }
        }

        public class Float : INumber
        {
            private float val;

            [Inject]
            public Float(float v)
            {
                val = v;
            }

            public int CompareTo(object obj)
            {
                if (!(obj is Float))
                {
                    return -1;
                }

                if (val == ((Float)obj).val)
                {
                    return 0;
                }

                if (val < ((Float)obj).val)
                {
                    return -1;
                }

                return 1;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Float))
                {
                    return false;
                }

                if (this.val == ((Float)obj).val)
                {
                    return true;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return val.GetHashCode();
            }
        }
        
        public class Integer1 : INumber
        {
            private int val;

            [Inject]
            public Integer1([Parameter(typeof(NamedInt))] int v)
            {
                val = v;
            }

            public int CompareTo(object obj)
            {
                if (!(obj is Integer1))
                {
                    return -1;
                }
                if (this.val == ((Integer1)obj).val)
                {
                    return 0;
                }

                if (this.val < ((Integer1)obj).val)
                {
                    return -1;
                }

                return 1;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Integer1))
                {
                    return false;
                }

                if (this.val == ((Integer1)obj).val)
                {
                    return true;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return val.GetHashCode();
            }

            [NamedParameter]
            public class NamedInt : Name<int>
            {
            }
        }

        public class Integer2 : INumber
        {
            private int val;

            [Inject]
            public Integer2()
            {
                val = 0;
            }

            public int CompareTo(object obj)
            {
                if (!(obj is Integer2))
                {
                    return -1;
                }
                if (this.val == ((Integer2)obj).val)
                {
                    return 0;
                }

                if (this.val < ((Integer2)obj).val)
                {
                    return -1;
                }

                return 1;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Integer2))
                {
                    return false;
                }

                if (this.val == ((Integer2)obj).val)
                {
                    return true;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return val.GetHashCode();
            }
        }

        public class Integer3 : INumber
        {
            private int val;

            [Inject]
            public Integer3([Parameter(typeof(NamedInt))] int v)
            {
                val = v;
            }

            public int CompareTo(object obj)
            {
                if (!(obj is Integer))
                {
                    return -1;
                }
                if (this.val == ((Integer3)obj).val)
                {
                    return 0;
                }

                if (this.val < ((Integer3)obj).val)
                {
                    return -1;
                }

                return 1;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Integer3))
                {
                    return false;
                }

                if (this.val == ((Integer3)obj).val)
                {
                    return true;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return val.GetHashCode();
            }

            [NamedParameter]
            public class NamedInt : Name<int>
            {
            }
        }

        public class Float1 : INumber
        {
            private float val;

            [Inject]
            public Float1([Parameter(typeof(NamedFloat))] float v)
            {
                val = v;
            }

            public int CompareTo(object obj)
            {
                if (!(obj is Float))
                {
                    return -1;
                }

                if (val == ((Float1)obj).val)
                {
                    return 0;
                }

                if (val < ((Float1)obj).val)
                {
                    return -1;
                }

                return 1;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Float1))
                {
                    return false;
                }

                if (this.val == ((Float1)obj).val)
                {
                    return true;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return val.GetHashCode();
            }

            [NamedParameter]
            public class NamedFloat : Name<float>
            {
            }
        }

        public class Pool1
        {
            [Inject]
            private Pool1([Parameter(typeof(SetOfClasseWithDefault))] ISet<INumber> numbers)
            {
                this.Numbers = numbers;
            }

            public ISet<INumber> Numbers { get; set; }
        }

        [NamedParameter(DefaultClass = typeof(Integer2))]
        public class SetOfClasseWithDefault : Name<ISet<INumber>>
        {
        }
    }

    public class Timeshift : ITimeshift
    {
        [Inject]
        public Timeshift([Parameter(typeof(TimeshiftLinkId))] string linkId, [Parameter(typeof(TimeshiftInTicks))] long timeshiftInTicks)
        {
            this.LinkId = linkId;
            this.TimeshiftSpan = TimeSpan.FromTicks(timeshiftInTicks);
        }

        public string LinkId { get; private set; }

        public TimeSpan TimeshiftSpan { get; private set; }

        [NamedParameter("TimeshiftLinkId", "TimeshiftLinkId", "myid")]
        public class TimeshiftLinkId : Name<string>
        {
        }

        [NamedParameter("TimeshiftInTicks", "TimeshiftInTicks", "10")]
        public class TimeshiftInTicks : Name<long>
        {
        }
    }

    public class Timeshift1 : ITimeshift
    {
        [Inject]
        public Timeshift1([Parameter(typeof(TimeshiftLinkId))] string linkId, [Parameter(typeof(TimeshiftInTicks))] long timeshiftInTicks)
        {
            this.LinkId = linkId;
            this.TimeshiftSpan = TimeSpan.FromTicks(timeshiftInTicks);
        }

        public string LinkId { get; private set; }

        public TimeSpan TimeshiftSpan { get; private set; }

        [NamedParameter("TimeshiftLinkId1", "TimeshiftLinkId1", "myid")]
        public class TimeshiftLinkId : Name<string>
        {
        }

        [NamedParameter("TimeshiftInTicks1", "TimeshiftInTicks1", "10")]
        public class TimeshiftInTicks : Name<long>
        {
        }
    }

    [NamedParameter(DefaultClass = typeof(Timeshift))]
    public class SetOfTimeshifts : Name<ISet<ITimeshift>>
    {
    }

    public class SetofTimeShiftClass
    {
        [Inject]
        public SetofTimeShiftClass([Parameter(typeof(SetOfTimeshifts))] ISet<ITimeshift> timeshifts)
        {
            this.Timeshifts = timeshifts;
        }
    
        public ISet<ITimeshift> Timeshifts { get; set; }
    }
    [NamedParameter]
    public class SetOfTimeshiftsWithoutDefaultClass : Name<ISet<ITimeshift>>
    {
    }

    public class SetofTimeShiftClassWithoutDefault
    {
        [Inject]
        public SetofTimeShiftClassWithoutDefault([Parameter(typeof(SetOfTimeshiftsWithoutDefaultClass))] ISet<ITimeshift> timeshifts)
        {
            this.Timeshifts = timeshifts;
        }

        public ISet<ITimeshift> Timeshifts { get; set; }
    }
}