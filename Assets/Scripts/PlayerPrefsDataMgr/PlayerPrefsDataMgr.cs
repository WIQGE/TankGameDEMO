using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;

/// <summary>
/// 数据管理类，同一管理数据存储和读取
/// </summary>
public class PlayerPrefsDataMgr 
{
    private static PlayerPrefsDataMgr instance = new PlayerPrefsDataMgr();
    
    public static PlayerPrefsDataMgr Instance
    {
        get
        { 
            return instance;
        }
    }

    private PlayerPrefsDataMgr()
    {

    }

    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="data">数据对象</param>
    /// <param name="keyName">数据唯一标识key</param>
    public void SaveData(object data, string keyName)
    {
        Type dataType = data.GetType();
        FieldInfo[] infos = dataType.GetFields();


        //保证数据唯一性
        //自己定规则 keyName_数据类型_字段类型_字段名
        string saveKeyName = "";
        FieldInfo info;
        for (int i = 0; i < infos.Length; i++)
        {
            //得到具体的字段信息
            info = infos[i];
            //通过FieldInfo可以得到字段类型 和字段的名字
            //info.FieldType.Name
            //info.Name

            //根据定制规则 来进行key的生成
            saveKeyName = keyName + "_" + dataType.Name + "_" + info.FieldType.Name + "_" + info.Name;
            //得到了key 接下来就要 存储了
            SaveValue(info.GetValue(data), saveKeyName);
            
        }
        PlayerPrefs.Save();
    }

    private void SaveValue(object value,string keyName)
    {
        //判断数据的类型
        Type fieldType = value.GetType();
        //判断是不是int
        if(fieldType == typeof(int) )
        {
            //Debug.Log("存储int" + keyName);
            PlayerPrefs.SetInt(keyName, (int)value);
        }
        else if(fieldType == typeof(float) )
        {
            PlayerPrefs.SetFloat(keyName, (float)value);
        }
        else if (fieldType == typeof(string) )
        {
            PlayerPrefs.SetString(keyName, value.ToString());
        }
        else if(fieldType == typeof(bool))
        { 
            //自定义bool值规则
            PlayerPrefs.SetInt(keyName, (bool)value ? 1 : 0);
        }
        //如何判断泛型类型
        //通过反射 判断 父子关系
        //判断 是不是 IList的子类（因为List类型是IList的子类）
        //相当于一个父亲有一个儿子，这个儿子 是程序员、司机、教师、厨师
        //但我们只需要判断他是不是这个父亲的儿子就可以了 不用具体的判断他是什么人
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            //父类装子类
            IList list = value as IList;
            //先存储数量
            PlayerPrefs.SetInt(keyName, list.Count);
            int index = 0;
            foreach (object obj in list)
            {
                //存储具体的值
                SaveValue(obj, keyName + index);
                index++;
            }
        }
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            IDictionary dic = value as IDictionary;
            PlayerPrefs.SetInt(keyName, dic.Count);
            int index = 0;
            foreach (object key in dic.Keys)
            {
                //存储具体的值
                SaveValue(key, keyName + "_key_" + index);
                SaveValue(dic[key], keyName + "_value_" + index);
                index++;
            }
        }
        //基础类型都不是 那么可能是自定义类型
        else
        {
            SaveData(value, keyName);
        }
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="type">想要读取的数据类型</param>
    /// <param name="keyName">数据唯一标识key</param>
    /// <returns></returns>
    public object LoadData(Type type,string keyName)
    {
        //不用object 而使用type
        //主要是节约外部一行代码

        //根据传入的的type创建一个对象 存储数据
        object data = Activator.CreateInstance(type);
        //要往new出来的对象中填充数据
        //得到所有字段
        FieldInfo[] infos = type.GetFields();

        string loadKeyName = "";
        FieldInfo info;
        for (int i = 0; i < infos.Length; i++)
        {
            info = infos[i];
            //key的拼接规则一定和存储时一模一样 这样才能找到数据
            loadKeyName = keyName + "_" + type.Name + "_" + info.FieldType.Name + "_" + info.Name;

            //有key 结合playerPrefs 来读取数据
            //填充数据到data中
            info.SetValue(data, LoadValue(info.FieldType, loadKeyName));
        }


        return data;
    }
    /// <summary>
    /// 得到单个数据的方法
    /// </summary>
    /// <param name="fieldType">类型，用于判断用哪个api来读取</param>
    /// <param name="keyName">用于获取具体数据的key</param>
    /// <returns></returns>
    private object LoadValue(Type fieldType, string keyName)
    {
        //根据字段类型 来判断 用哪个api来读取
        if (fieldType == typeof(int))
        {
            return PlayerPrefs.GetInt(keyName);
        }
        else if (fieldType == typeof(float))
        {
            return PlayerPrefs.GetFloat(keyName);
        }
        else if (fieldType == typeof(string))
        {
            return PlayerPrefs.GetString(keyName);
        }
        else if (fieldType == typeof(bool))
        {
            //根据自定义bool存储规则
            return PlayerPrefs.GetInt(keyName) == 1 ? true : false;
        }
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            //得到长度
            int count = PlayerPrefs.GetInt(keyName);
            //实例化一个IList对象 进行复制
            IList list = Activator.CreateInstance(fieldType) as IList;
            for (int i = 0; i < count; i++)
            {
                //目的是要得到list中泛型的类型
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + i));
            }
            return list;
        }
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            int count = PlayerPrefs.GetInt(keyName);
            IDictionary dic = Activator.CreateInstance(fieldType) as IDictionary;
            Type[] kvType = fieldType.GetGenericArguments();

            for (int i = 0; i < count; i++)
            {
                dic.Add(LoadValue(kvType[0], keyName + "_" + i), LoadValue(kvType[1], keyName + "_value_" + i));
            }
            return dic;
        }
        else
        {
            LoadData(fieldType, keyName);
        }
        return null;
    }
}
