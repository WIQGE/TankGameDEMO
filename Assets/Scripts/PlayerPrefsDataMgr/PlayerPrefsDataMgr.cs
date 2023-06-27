using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;

/// <summary>
/// ���ݹ����࣬ͬһ�������ݴ洢�Ͷ�ȡ
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
    /// �洢����
    /// </summary>
    /// <param name="data">���ݶ���</param>
    /// <param name="keyName">����Ψһ��ʶkey</param>
    public void SaveData(object data, string keyName)
    {
        Type dataType = data.GetType();
        FieldInfo[] infos = dataType.GetFields();


        //��֤����Ψһ��
        //�Լ������� keyName_��������_�ֶ�����_�ֶ���
        string saveKeyName = "";
        FieldInfo info;
        for (int i = 0; i < infos.Length; i++)
        {
            //�õ�������ֶ���Ϣ
            info = infos[i];
            //ͨ��FieldInfo���Եõ��ֶ����� ���ֶε�����
            //info.FieldType.Name
            //info.Name

            //���ݶ��ƹ��� ������key������
            saveKeyName = keyName + "_" + dataType.Name + "_" + info.FieldType.Name + "_" + info.Name;
            //�õ���key ��������Ҫ �洢��
            SaveValue(info.GetValue(data), saveKeyName);
            
        }
        PlayerPrefs.Save();
    }

    private void SaveValue(object value,string keyName)
    {
        //�ж����ݵ�����
        Type fieldType = value.GetType();
        //�ж��ǲ���int
        if(fieldType == typeof(int) )
        {
            //Debug.Log("�洢int" + keyName);
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
            //�Զ���boolֵ����
            PlayerPrefs.SetInt(keyName, (bool)value ? 1 : 0);
        }
        //����жϷ�������
        //ͨ������ �ж� ���ӹ�ϵ
        //�ж� �ǲ��� IList�����ࣨ��ΪList������IList�����ࣩ
        //�൱��һ��������һ�����ӣ�������� �ǳ���Ա��˾������ʦ����ʦ
        //������ֻ��Ҫ�ж����ǲ���������׵Ķ��ӾͿ����� ���þ�����ж�����ʲô��
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            //����װ����
            IList list = value as IList;
            //�ȴ洢����
            PlayerPrefs.SetInt(keyName, list.Count);
            int index = 0;
            foreach (object obj in list)
            {
                //�洢�����ֵ
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
                //�洢�����ֵ
                SaveValue(key, keyName + "_key_" + index);
                SaveValue(dic[key], keyName + "_value_" + index);
                index++;
            }
        }
        //�������Ͷ����� ��ô�������Զ�������
        else
        {
            SaveData(value, keyName);
        }
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="type">��Ҫ��ȡ����������</param>
    /// <param name="keyName">����Ψһ��ʶkey</param>
    /// <returns></returns>
    public object LoadData(Type type,string keyName)
    {
        //����object ��ʹ��type
        //��Ҫ�ǽ�Լ�ⲿһ�д���

        //���ݴ���ĵ�type����һ������ �洢����
        object data = Activator.CreateInstance(type);
        //Ҫ��new�����Ķ������������
        //�õ������ֶ�
        FieldInfo[] infos = type.GetFields();

        string loadKeyName = "";
        FieldInfo info;
        for (int i = 0; i < infos.Length; i++)
        {
            info = infos[i];
            //key��ƴ�ӹ���һ���ʹ洢ʱһģһ�� ���������ҵ�����
            loadKeyName = keyName + "_" + type.Name + "_" + info.FieldType.Name + "_" + info.Name;

            //��key ���playerPrefs ����ȡ����
            //������ݵ�data��
            info.SetValue(data, LoadValue(info.FieldType, loadKeyName));
        }


        return data;
    }
    /// <summary>
    /// �õ��������ݵķ���
    /// </summary>
    /// <param name="fieldType">���ͣ������ж����ĸ�api����ȡ</param>
    /// <param name="keyName">���ڻ�ȡ�������ݵ�key</param>
    /// <returns></returns>
    private object LoadValue(Type fieldType, string keyName)
    {
        //�����ֶ����� ���ж� ���ĸ�api����ȡ
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
            //�����Զ���bool�洢����
            return PlayerPrefs.GetInt(keyName) == 1 ? true : false;
        }
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            //�õ�����
            int count = PlayerPrefs.GetInt(keyName);
            //ʵ����һ��IList���� ���и���
            IList list = Activator.CreateInstance(fieldType) as IList;
            for (int i = 0; i < count; i++)
            {
                //Ŀ����Ҫ�õ�list�з��͵�����
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
