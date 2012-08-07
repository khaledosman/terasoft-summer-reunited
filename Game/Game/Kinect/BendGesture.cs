﻿using Microsoft.Kinect;
using Game.Text;
using System.Diagnostics;

namespace Game.Kinect
{ 
    class BendGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if (skeleton.Joints[JointType.HipCenter].Position.Y < (Constants.posY-0.5))
            {
                return GesturePartResult.Suceed;
            }
            else return GesturePartResult.Fail;
        }
    }


}