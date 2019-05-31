using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    class Camera
    {
        public BOX box;
        public float mAngleX = 0;
        public float mAngleY = 0;
        vec3 mDirection;
        vec3 mPosition;
        public vec3 mCenter;
        vec3 mRight;
        vec3 mUp;
        mat4 mViewMatrix;
        mat4 mProjectionMatrix;
        public Camera()
        {
            Reset(0, 0, 5, 0, 0, 0, 0, 1, 0);
            SetProjectionMatrix(45, 4 / 3, 0.1f, 1000);
        }

        public vec3 GetLookDirection()
        {
            return mDirection;
        }

        public mat4 GetViewMatrix()
        {
            return mViewMatrix;
        }

        public mat4 GetProjectionMatrix()
        {
            return mProjectionMatrix;
        }
        public vec3 GetCameraPosition()
        {
            return mPosition;
        }

        public vec3 GetCameraTarget()
        {
            return mCenter;
        }
        public void Reset(float eyeX, float eyeY, float eyeZ, float centerX, float centerY, float centerZ, float upX, float upY, float upZ)
        {
            vec3 eyePos = new vec3(eyeX, eyeY, eyeZ);
            mCenter = new vec3(centerX, centerY+35, centerZ);
            vec3 upVec = new vec3(upX, upY, upZ);

            mPosition = eyePos;
            mDirection = mCenter - mPosition;
            mRight = glm.cross(mDirection, upVec);
            mUp = upVec;
            mUp = glm.normalize(mUp);
            mRight = glm.normalize(mRight);
            mDirection = glm.normalize(mDirection);

            mViewMatrix = glm.lookAt(mPosition, mCenter, mUp);
            //box
            box = new BOX(mCenter);
        }
        public void SetHeight(float y)
        {
            mCenter.y = y;
        }
        public void UpdateViewMatrix()
        {
            mDirection = new vec3((float)(-Math.Cos(mAngleY) * Math.Sin(mAngleX))
                , (float)(Math.Sin(mAngleY))
                , (float)(-Math.Cos(mAngleY) * Math.Cos(mAngleX)));
            mRight = glm.cross(mDirection, new vec3(0, 1, 0));
            mUp = glm.cross(mRight, mDirection);

            mPosition = mCenter - (mDirection)*6;
            //mPosition.y += 5;

            mViewMatrix = glm.lookAt(mPosition, mCenter, mUp);
        }
        public void SetProjectionMatrix(float FOV, float aspectRatio, float near, float far)
        {
            mProjectionMatrix = glm.perspective(FOV, aspectRatio, near, far);
        }


        public void Yaw(float angleDegrees)
        {
            mAngleX += angleDegrees;
        }

        public void Pitch(float angleDegrees)
        {
            //mAngleY += angleDegrees;
        }

        public void Walk(float dist)
        {
            vec3 ay7aga = mCenter + (mDirection * dist);
            if (ay7aga.x < 490 && ay7aga.x > -490 && ay7aga.y <490 && ay7aga.y > 0 && ay7aga.z < 495 && ay7aga.z > -495)
            {
                mCenter += dist * mDirection;
                
            }
            else
                mCenter -= dist * mDirection;
               
           // mCenter += dist * mDirection;
        }
        public void Strafe(float dist)
        {
            vec3 ay7aga = mCenter + (mRight * dist);
            if (ay7aga.x < 490 && ay7aga.x > -490 && ay7aga.y < 490 && ay7aga.y > 0 && ay7aga.z < 495 && ay7aga.z > -495)
            {
                mCenter += dist * mRight;
            }
            else
                mCenter -= dist * mRight;
            //mCenter += dist * mRight;
        }
        public void Fly(float dist)
        {
            vec3 ay7aga = mCenter + (mUp * dist);
            if (ay7aga.x < 200 * 5 && ay7aga.x > -200 && ay7aga.y < 200 && ay7aga.y > 0 && ay7aga.z < 200 && ay7aga.z > -200)
            {
                mCenter += dist * mUp;
            }
            else
                mCenter -= dist * mUp;
            //mCenter += dist * mUp;
        }
    }
}
