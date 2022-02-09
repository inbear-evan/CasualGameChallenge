using UnityEngine;

namespace Yellotail.InGame
{
    public class InGameCameraInput : CameraInput
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        //Mouse input axes;
        public string mouseHorizontalAxis = "Mouse X";
        public string mouseVerticalAxis = "Mouse Y";
        public string mouseZoom = "Mouse ScrollWheel";

        //Invert input options;
        public bool invertHorizontalInput = false;
        public bool invertVerticalInput = false;
        public bool invertZoomInput = false;

        //Use this value to fine-tune mouse movement;
        //All mouse input will be multiplied by this value;
        public float mouseInputMultiplier = 0.01f;

        public override float GetHorizontalCameraInput()
        {
            if (Input.GetMouseButton(0))
                return GetHorizontal(Input.GetAxisRaw(this.mouseHorizontalAxis));
            else
                return 0;
        }

        private float GetHorizontal(float input) // -1 ~ 1
        {
            //Since raw mouse input is already time-based, we need to correct for this before passing the input to the camera controller;
            if (Time.timeScale > 0f && Time.deltaTime > 0f)
            {
                input /= Time.deltaTime;
                input *= Time.timeScale;
            }
            else
                input = 0f;

            //Apply mouse sensitivity;
            input *= this.mouseInputMultiplier;

            //Invert input;
            if (this.invertHorizontalInput)
                input *= -1f;

            return input;
        }

        public override float GetVerticalCameraInput()
        {
            if (Input.GetMouseButton(0))
                return GetVertical(-Input.GetAxisRaw(this.mouseVerticalAxis));
            else
                return 0;
        }

        private float GetVertical(float input) // -1 ~ 1
        {
            //Since raw mouse input is already time-based, we need to correct for this before passing the input to the camera controller;
            if (Time.timeScale > 0f && Time.deltaTime > 0f)
            {
                input /= Time.deltaTime;
                input *= Time.timeScale;
            }
            else
                input = 0f;

            //Apply mouse sensitivity;
            input *= this.mouseInputMultiplier;

            //Invert input;
            if (this.invertVerticalInput)
                input *= -1f;

            return input;
        }

        public override float GetZoomCameraInput()
        {
            throw new System.NotImplementedException();
        }
#else
        public float mobileSpeed = 3f;

        public float MobileHorizontalAxis { get; private set; } = 0f;
        public float MobileVerticalAxis { get; private set; } = 0f;

        private Vector2 prevPosition;

        private enum HandleState
        {
            None,
            Dragging,
            ZoomInOut
        }
        private HandleState state;

        private void LateUpdate()
        {
            HandleDrag();
            HandleZoom();
        }

        public override float GetHorizontalCameraInput()
        {
            return GetHorizontal(MobileHorizontalAxis);
        }

        public override float GetVerticalCameraInput()
        {
            return GetVertical(MobileVerticalAxis);
        }

        public override float GetZoomCameraInput()
        {
            throw new System.NotImplementedException();
        }

        private void HandleDrag()
        {
            if (state.Equals(HandleState.None))
            {
                if (Input.touchCount > 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        var touch = Input.GetTouch(i);
                        if (UnityHelper.IsPointerOverGameObject(touch.position))
                            continue;

                        if (touch.phase == TouchPhase.Began)
                        {
                            var nowPosition = touch.position;
                            prevPosition = nowPosition;
                            state = HandleState.Dragging;
                        }
                    }
                }
            }

            if (state.Equals(HandleState.Dragging))
            {
                var touch = GetDraggingTouch();
                var nowPosition = touch.position;

                var delta = nowPosition - prevPosition;
                var result = delta * Time.deltaTime * mobileSpeed;

                MobileHorizontalAxis = result.x;
                MobileVerticalAxis = -result.y;

                prevPosition = nowPosition;

                if (touch.phase == TouchPhase.Ended)
                {
                    MobileHorizontalAxis = 0f;
                    MobileVerticalAxis = 0f;
                    state = HandleState.None;
                }
            }
        }

        private Touch GetDraggingTouch()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.GetTouch(i);
                if (UnityHelper.IsPointerOverGameObject(touch.position))
                    continue;

                return touch;
            }

            return Input.GetTouch(0);
        }

        private void HandleZoom()
        {
            // TO DO : Zoom In & Out
        }

        private float GetHorizontal(float input) // -1 ~ 1
        {
            //Since raw mouse input is already time-based, we need to correct for this before passing the input to the camera controller;
            if (Time.timeScale > 0f && Time.deltaTime > 0f)
            {
                input /= Time.deltaTime;
                input *= Time.timeScale;
            }
            else
                input = 0f;

            return input;
        }

        private float GetVertical(float input) // -1 ~ 1
        {
            //Since raw mouse input is already time-based, we need to correct for this before passing the input to the camera controller;
            if (Time.timeScale > 0f && Time.deltaTime > 0f)
            {
                input /= Time.deltaTime;
                input *= Time.timeScale;
            }
            else
                input = 0f;

            return input;
        }
#endif
    }
}
