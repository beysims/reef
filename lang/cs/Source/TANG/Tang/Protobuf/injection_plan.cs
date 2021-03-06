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

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: injection_plan.proto
namespace InjectionPlanProto
{
    [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"InjectionPlan")]
    public partial class InjectionPlan : global::ProtoBuf.IExtensible
    {
        public InjectionPlan() { }

        private string _name;
        [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name = @"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        private Constructor _constructor = null;
        [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"constructor", DataFormat = global::ProtoBuf.DataFormat.Default)]
        [global::System.ComponentModel.DefaultValue(null)]
        public Constructor constructor
        {
            get { return _constructor; }
            set { _constructor = value; }
        }
        private Instance _instance = null;
        [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name = @"instance", DataFormat = global::ProtoBuf.DataFormat.Default)]
        [global::System.ComponentModel.DefaultValue(null)]
        public Instance instance
        {
            get { return _instance; }
            set { _instance = value; }
        }
        private Subplan _subplan = null;
        [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name = @"subplan", DataFormat = global::ProtoBuf.DataFormat.Default)]
        [global::System.ComponentModel.DefaultValue(null)]
        public Subplan subplan
        {
            get { return _subplan; }
            set { _subplan = value; }
        }
        private global::ProtoBuf.IExtension extensionObject;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }

    [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"Subplan")]
    public partial class Subplan : global::ProtoBuf.IExtensible
    {
        public Subplan() { }

        private int _selected_plan = default(int);
        [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"selected_plan", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
        [global::System.ComponentModel.DefaultValue(default(int))]
        public int selected_plan
        {
            get { return _selected_plan; }
            set { _selected_plan = value; }
        }
        private readonly global::System.Collections.Generic.List<InjectionPlan> _plans = new global::System.Collections.Generic.List<InjectionPlan>();
        [global::ProtoBuf.ProtoMember(2, Name = @"plans", DataFormat = global::ProtoBuf.DataFormat.Default)]
        public global::System.Collections.Generic.List<InjectionPlan> plans
        {
            get { return _plans; }
        }

        private global::ProtoBuf.IExtension extensionObject;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }

    [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"Constructor")]
    public partial class Constructor : global::ProtoBuf.IExtensible
    {
        public Constructor() { }

        private readonly global::System.Collections.Generic.List<InjectionPlan> _args = new global::System.Collections.Generic.List<InjectionPlan>();
        [global::ProtoBuf.ProtoMember(1, Name = @"args", DataFormat = global::ProtoBuf.DataFormat.Default)]
        public global::System.Collections.Generic.List<InjectionPlan> args
        {
            get { return _args; }
        }

        private global::ProtoBuf.IExtension extensionObject;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }

    [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"Instance")]
    public partial class Instance : global::ProtoBuf.IExtensible
    {
        public Instance() { }

        private string _value;
        [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name = @"value", DataFormat = global::ProtoBuf.DataFormat.Default)]
        public string value
        {
            get { return _value; }
            set { _value = value; }
        }
        private global::ProtoBuf.IExtension extensionObject;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }

}