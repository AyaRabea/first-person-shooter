using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace Graphics
{
   

    class BOX
    {

        public float diffx, diffz;
        vec3 default_min;
        vec3 default_max;
        public vec3 dynamic_min;
        public vec3 dynamic_max;

       
        //float midx, midy, midz;
        public float width, height,depth;

        vec3 default_mid;
        public vec3 dynamic_mid;
        public BOX(vec3 player_pos)
        {
            default_min = new vec3(50000f, 50000f, 50000f);
            default_max = new vec3(-50000f, -50000f, -50000f);

            default_mid = new vec3();
            dynamic_mid = new vec3();
          

            default_mid.x = player_pos.x;
            default_mid.y = player_pos.y;
            default_mid.z = player_pos.z;

            width = 50f ;
            height =60f ;
            depth =60f;
            


        }
     

        public BOX(List<vec3> vert)  //for   md2 , md2lol objects
        {
            default_min = new vec3(50000f, 50000f, 50000f);
            default_max = new vec3(-50000f, -50000f, -50000f);

            dynamic_min = new vec3();
            dynamic_max = new vec3();

            default_mid = new vec3();
            //dynamic_mid = new vec3();
           

            calcmin_max(vert);

            width = default_max.x - default_min.x;
            height = default_max.y - default_min.y;
            depth = default_max.z - default_min.z;


            default_mid.x = (default_min.x + default_max.x) / 2;
            default_mid.y = (default_min.y + default_max.y) / 2;
            default_mid.z = (default_min.z + default_max.z) / 2;

        }
       
        public BOX(vec3 min,vec3 max)  //for model3D objects
        {
            default_min = new vec3();
            default_max = new vec3();


            default_mid = new vec3();
            dynamic_mid = new vec3();

            dynamic_min = new vec3();
            dynamic_max = new vec3();
            
           

            width = default_max.x - default_min.x;
            height = default_max.y - default_min.y;
            depth = default_max.z - default_min.z;

            
           default_mid.x = (default_min.x + default_max.x) / 2;
           default_mid.y = (default_min.y + default_max.y) / 2;
           default_mid.z = (default_min.z + default_max.z) / 2;

        }
        
        
        public void calcmin_max(List<vec3> vertices)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (default_min.x > vertices[i].x)
                {
                    default_min.x = vertices[i].x;
                }
                if (default_max.x < vertices[i].x)
                {
                    default_max.x = vertices[i].x;
                }
                //y
                if (default_min.y > vertices[i].y)
                {
                    default_min.y = vertices[i].y;
                }
                if (default_max.y < vertices[i].y)
                {
                    default_max.y = vertices[i].y;
                }
                //z
                if (default_min.z > vertices[i].z)
                {
                    default_min.z = vertices[i].z;
                }
                if (default_max.z < vertices[i].z)
                {
                    default_max.z = vertices[i].z;
                }

            }

        }
        

        public vec3 Multi(mat4 a, vec3 b)
        {
            

            vec3 res = new vec3(0, 0, 0);
            res.x = a[0, 0] * b.x + a[1, 0] * b.y + a[2, 0] * b.z + a[3, 0] * 1;
            res.y = a[0, 1] * b.x + a[1, 1] * b.y + a[2, 1] * b.z + a[3, 1] * 1;
            res.z = a[0, 2] * b.x + a[1, 2] * b.y + a[2, 2] * b.z + a[3, 2] * 1;

            return res;
        }


        public void update_mid(vec3 pos) //for player
        {
           mat4 transmatrix = glm.translate(new mat4(1), pos);
           dynamic_mid = Multi(transmatrix, default_mid);

        }

        public void update_mid(mat4 scalemat,mat4 rotmat,mat4 transmat)
        {
            List<mat4> modelmatrices = new List<mat4>() { scalemat,rotmat, transmat };
            mat4 transformationMatrix = MathHelper.MultiplyMatrices(modelmatrices);

            //dynamic=mat *default_vec
            dynamic_min = Multi(transformationMatrix, default_min);
            dynamic_max = Multi(transformationMatrix, default_max);
           

            dynamic_mid.x = (dynamic_min.x + dynamic_max.x) / 2;
            dynamic_mid.y = (dynamic_min.y + dynamic_max.y) / 2;
            dynamic_mid.z = (dynamic_min.z + dynamic_max.z) / 2;
             

            width = dynamic_max.x - dynamic_min.x;
            height = dynamic_max.y - dynamic_min.y;
            depth = dynamic_max.z - dynamic_min.z;

        }
        

        public bool is_collied(BOX obj,move_direction move_dir,vec3 camlook)
        {
            float distx = Math.Abs((dynamic_mid.x - obj.dynamic_mid.x));
            float disty = Math.Abs((dynamic_mid.y - obj.dynamic_mid.y));
            float distz = Math.Abs((dynamic_mid.z - obj.dynamic_mid.z));

            if (distx < (width / 2 + obj.width / 2)+15  &&
                distz < (depth / 2 + obj.depth / 2)+15)
            {
                //collision happen
                //if (camlook.x <0.0)
                {
                    if (move_dir == move_direction.RIGHT_X)
                    {
                        if (dynamic_mid.x > obj.dynamic_mid.x) //player is left to obj
                        {
                            return true;
                        }

                    }

                    else if (move_dir == move_direction.LEFT_X)
                    {
                        if (dynamic_mid.x < obj.dynamic_mid.x) //player is right to obj
                        {
                            return true;
                        }

                    }
                }

                /*else if (camlook.x >= 0.0)
                {
                    if (move_dir == move_direction.RIGHT_X)
                    {
                        if (dynamic_mid.x < obj.dynamic_mid.x) //player is left to obj
                        {
                            return true;
                        }

                    }

                    else if (move_dir == move_direction.LEFT_X)
                    {
                        if (dynamic_mid.x > obj.dynamic_mid.x) //player is right to obj
                        {
                            return true;
                        }

                    }
                }*/
                //--------------------------------------------------------------//

                if (camlook.z <= 0.0)
                {
                    if (move_dir == move_direction.FRONT_Z)
                    {
                        if (dynamic_mid.z > obj.dynamic_mid.z) //object further than player
                        {
                            return true;
                        }

                    }
                    else if (move_dir == move_direction.BACK_Z)
                    {
                        if (dynamic_mid.z < obj.dynamic_mid.z) //object back to palyer(player rag3 b dhr 3la al obj)
                        {
                            return true;
                        }

                    }
                }

                else if (camlook.z > 0.0)
                {
                    if (move_dir == move_direction.FRONT_Z)
                    {
                        if (dynamic_mid.z < obj.dynamic_mid.z) //object further than player
                        {
                            return true;
                        }

                    }
                    else if (move_dir == move_direction.BACK_Z)
                    {
                        if (dynamic_mid.z > obj.dynamic_mid.z) //object back to palyer(player rag3 b dhr 3la al obj)
                        {
                            return true;
                        }

                    }
                }

                return false;
            }
            else
            {
                //diffx = distx - (width / 2 + obj.width / 2);
                //diffz = distz - (depth / 2 + obj.depth / 2);
                return false;
            }

        }
        
        


    }
}

    

