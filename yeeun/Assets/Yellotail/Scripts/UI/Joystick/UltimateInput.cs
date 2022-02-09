using CMF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public class UltimateInput : CharacterInput
    {
		public string joystickName = "Movement";
        public string jumpActionName = "Jump";
		
		public KeyCode jumpKey = KeyCode.Space;
		

		//If this is enabled, Unity's internal input smoothing is bypassed;
		public bool useRawInput = true;

		public override float GetHorizontalMovementInput()
		{
			if (useRawInput)
				return UltimateJoystick.GetHorizontalAxisRaw(joystickName);
			else
				return UltimateJoystick.GetHorizontalAxis(joystickName);
		}
		public override float GetVerticalMovementInput()
		{
			if (useRawInput)
				return UltimateJoystick.GetVerticalAxisRaw(joystickName);
			else
				return UltimateJoystick.GetVerticalAxis(joystickName);
		}

		public override bool IsJumpKeyPressed()
        {
            return UltimateAction.IsActionPressed(jumpActionName);
        }
	}
}
