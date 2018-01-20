using UnityEngine;

namespace BCM_Apps
{
    public class Utilities
    {
        public static bool touchCheck()
        {
            if (Input.touchCount > 0)
                return true;
            return false;
        }

        public static bool isTouchingObject(string ObjectName)
        {
            ///<summary>
            ///To use this, first create an 'if' statement with the condition 'touchCheck()', 
            ///and within this if statement, create a second 'if' statement with the condition checking against 'Input.GetTouch(i).phase'.
            ///Finally, create a third 'if' statement with the condition 'isTouchingObject', and if this returns true, do stuff.  
            ///</summary>

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000F))
                if (hit.transform.gameObject.name == ObjectName)
                    return true;
            return false;
        }
    }
}


