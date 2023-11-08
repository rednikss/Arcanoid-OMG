using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Architecture.Data.DataParser
{
    public class JSONDataParser : MonoInstaller, IDataParser
    {
        public override void Init(ProjectContext.ProjectContext context)
        {
            context.GetContainer().SetServiceSelf<IDataParser>(this);
        }
        
        public TDataType Parse<TDataType>(in string data)
        {
            return JsonUtility.FromJson<TDataType>(data);
        }

        public string Convert<TDataType>(in TDataType data)
        {
            return JsonUtility.ToJson(data);
        }
    }
}