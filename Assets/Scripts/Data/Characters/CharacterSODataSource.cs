using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.Characters
{
    public class CharacterSODataSource : SerializedMonoBehaviour, ICharacterDataSource
    {
        [SerializeField] List<CharacterStatsSO> _characterStats;

        public ICharacterStats GetById(string id)
        {
            return _characterStats.FirstOrDefault(x => x.Id == id);
        }

        public ICharacterStats GetRandom()
        {

            return null;
        }
    }
}
