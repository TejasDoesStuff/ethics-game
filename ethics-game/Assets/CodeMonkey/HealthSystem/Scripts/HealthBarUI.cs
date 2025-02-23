using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.HealthSystemCM {

    /// <summary>
    /// Simple UI Health Bar, sets the Image fillAmount based on the value
    /// </summary>
    public class HealthBarUI : MonoBehaviour {

        [Tooltip("Image to show the Health Bar, should be set as Fill, the script modifies fillAmount")]
        [SerializeField] private Image image;

        private float _value;
        public float value // Modify this property to directly set the fill amount
        {
            get => _value;
            set
            {
                _value = value;
                UpdateHealthBar();
            }
        }

        /// <summary>
        /// Update Health Bar using the Image fillAmount based on the current value
        /// </summary>
        private void UpdateHealthBar() {
            image.fillAmount = _value / 100f;
        }
    }
}