using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.Characters
{
    public class CharacterDataSourceSO : SerializedMonoBehaviour, ICharacterDataSource
    {
        [SerializeField] List<CharacterStatsSO> _characterStats;

        public ICharacterStats GetById(string id)
        {
            return _characterStats.FirstOrDefault(x => x.Id == id.ToLower());
        }

        public ICharacterStats GetRandom()
        {

            return null;
        }
    }
}
