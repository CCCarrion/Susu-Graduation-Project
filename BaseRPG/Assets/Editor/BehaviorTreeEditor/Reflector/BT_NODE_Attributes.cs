using System;
using UnityEngine;
using System.Collections;
using System.Reflection;


namespace BT
{
    /// <summary>
    /// 行为树节点类型枚举
    /// </summary>
    public enum BT_NODE_TYPE
    {
         NONE = 0,
         COMPOSITE,
         DECORATOR,
         CONDITION,
         ACTION
    }


    //标识行为树节点类，包含标签说明等信息，用于编辑器显示以及行为树序列化反序列化的操作
    [AttributeUsage(AttributeTargets.Class)]
    public class BT_NODE_Attribute : Attribute
    {
        public BT_NODE_TYPE node_type;     //指示节点类型
        public string node_name;        //指示节点名称
        public string node_desc;        //节点说明

        private BT_NODE_PROPERTY_Attribute listProperty;    //属性特性列表


    }

    //标识行为树节点类的属性  包含标签说明等信息，用于编辑器显示以及行为树序列化反序列化的操作
    [AttributeUsage(AttributeTargets.Property)]
    public class BT_NODE_PROPERTY_Attribute : Attribute
    {
        public string label;     // 指示属性标签
        public string tip;     // 指示属性说明


    }



}
