using UnityEngine;

namespace FeoFun.Inputs
{
    public class InputManager : Singleton<InputManager>
    {
        private bool _touchBegan;
        private bool _touchEnded;
        private bool _anyKey;

        public bool TouchBegan => _touchBegan;
        public bool TouchEnded => _touchEnded;
        public bool AnyKey => _anyKey;

        private void Update()
        {
            if (Input.touchSupported)
            {
                _anyKey = Input.touchCount > 0;
                _touchBegan = Input.GetTouch(0).phase == TouchPhase.Began;
                _touchEnded = Input.GetTouch(0).phase == TouchPhase.Ended;
            }
            else
            {
                _anyKey = Input.anyKey;
                _touchBegan = Input.GetMouseButtonDown(0);
                _touchEnded = Input.GetMouseButtonUp(0);
            }
        }
    }
}
