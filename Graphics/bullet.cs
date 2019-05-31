using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
using System.IO;

namespace Graphics
{
    class bullet
    {
        //BOX box;
        public Model3D bull;
        //Camera c = new Camera();
        public vec3 pos;
        public vec3 dir;

        string projectPath =Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public bullet(vec3 camera_position,Camera came)
        {
            pos = camera_position;
            dir = came.GetLookDirection();


            bull = new Model3D();
            bull.LoadFile(projectPath + "\\ModelFiles//Textured Bullet OBJ", 17, "lowpolybullet.obj");


            bull.scalematrix = glm.scale(new mat4(1), new vec3(0.5f, 0.5f, 0.5f));
            bull.transmatrix = glm.translate(new mat4(1), pos);
            mat4 x = glm.rotate((float)((90.0f / 180) * Math.PI), new vec3(1, 0, 0));
            mat4 y = glm.rotate((float)came.mAngleX, new vec3(0, 1, 0));
            mat4 z = x * y;
            bull.rotmatrix = z;
            //bull.box.update_mid(bull.scalematrix, bull.rotmatrix, bull.transmatrix);
            //---------------------------------------------------------------------------------

        }

        public bullet(vec3 cp)
        {
            pos = cp;
           
        }

   /*     public vec3 b_move(vec3 cp,vec2 r)
        {

            r.x *= 8;
            r.y *= 8;
            cp.x += r.x;
            cp.z += r.y;
            return cp;
        }*/

        public void draw(Model3D m, int matID)
        {
            m.Draw(matID);
        }
        public void update()
        {
            pos.x += dir.x;
            pos.z += dir.z;
            bull.transmatrix = glm.translate(new mat4(1), pos);
            bull.box.update_mid(bull.scalematrix, bull.rotmatrix, bull.transmatrix);
        }
        

    }
}
