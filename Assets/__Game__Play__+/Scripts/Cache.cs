using UnityEngine;
using System.Collections.Generic;


public class Cache
{
    private static Dictionary<float, WaitForSeconds> m_WFS = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWFS(float key)
    {
        if (!m_WFS.ContainsKey(key))
        {
            m_WFS[key] = new WaitForSeconds(key);
        }

        return m_WFS[key];
    }

    //------------------------------------------------------------------------------------------------------------
    private static Dictionary<Collider, Floor> m_Floor = new Dictionary<Collider, Floor>();

    public static Floor Get_Floor_Script_From_Colider(Collider key)
    {
        if (!m_Floor.ContainsKey(key))
        {
            Floor burger = key.GetComponent<Floor>();

            if (burger != null)
            {
                m_Floor.Add(key, burger);
            }
            else
            {
                return null;
            }
        }

        return m_Floor[key];
    }
    //------------------------------------------------------------------------------------------------------------
    private static Dictionary<Collider, Colider3D_Player> m_Colider3D_Player = new Dictionary<Collider, Colider3D_Player>();

    public static Colider3D_Player Get_Colider3D_Player_Script_From_Colider(Collider key)
    {
        if (!m_Colider3D_Player.ContainsKey(key))
        {
            Colider3D_Player burger = key.GetComponent<Colider3D_Player>();

            if (burger != null)
            {
                m_Colider3D_Player.Add(key, burger);
            }
            else
            {
                return null;
            }
        }

        return m_Colider3D_Player[key];
    }
    //

}