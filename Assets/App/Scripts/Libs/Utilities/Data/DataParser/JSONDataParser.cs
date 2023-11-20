﻿using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Libs.Utilities.Data.DataParser
{
    public class JSONDataParser : MonoInstaller, IDataParser
    {
        public override void Init(ProjectContext.ProjectContext context)
        {
        }
        
        public TDataType Parse<TDataType>(string data)
        {
            return JsonUtility.FromJson<TDataType>(data);
        }

        public string Convert<TDataType>(TDataType data)
        {
            return JsonUtility.ToJson(data);
        }
    }
}