using UnityEngine;
namespace Kakky
{
    [CreateAssetMenu(fileName = "GameDataContext", menuName = "Kakky/GameDataContext")]
    public class GameDataContext : ScriptableObject
    {
        [SerializeField, Header("")] private PlayerParamData _playerParamData;
        public PlayerParamData PlayerParamData => _playerParamData;

        [SerializeField, Header("")] private StageData _stageData;
        public StageData StageData => _stageData;

        [SerializeField, Header("")] private TextData _textData;
        public TextData TextData => _textData;

        [SerializeField, Header("")] private StageDataBase _stageDataBase;
        public StageDataBase StageDataBase => _stageDataBase;

        [SerializeField, Header("")] private TimerData _timerData;
        public TimerData TimerData => _timerData;

        [SerializeField, Header("")] private GameOverData _gameOverData;
        public GameOverData GameOverData => _gameOverData;

        [SerializeField, Header("")] private Project.Key.SO.SumClearTimeData _sumClearTimeData;
        public Project.Key.SO.SumClearTimeData SumClearTimeData => _sumClearTimeData;
    }
}
