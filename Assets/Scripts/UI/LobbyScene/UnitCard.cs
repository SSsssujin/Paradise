using Paradise.Data.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Paradise.UI
{
    public class UnitCard : MonoBehaviour
    {
        private bool _isInit;
        private Image _image;
        
        private UnitData _unitData;

        // [수정] Init, reset, print 부분 구분하기
        public void Initialize(UnitData data)
        {
            if (!_isInit)
            {
                transform.ResetLocal();
                _image = transform.Find("Portrait").FetchComponent<Image>();
            }
            
            _unitData = data;
            PrintUnitInfo();
        }

        private void ResetData()
        {
            
        }

        private void PrintUnitInfo()
        {
            _image.sprite = _unitData.Portrait;
        }
    }
}