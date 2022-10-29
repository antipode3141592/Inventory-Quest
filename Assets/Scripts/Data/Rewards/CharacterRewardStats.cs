using UnityEngine;

namespace Data.Rewards
{
    public class CharacterRewardStats : IRewardStats
    {
        [SerializeField] string _characterId;

        public CharacterRewardStats(string characterId)
        {
            _characterId = characterId;
        }

        public string CharacterId => _characterId;
    }
}
