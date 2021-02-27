using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataManager
{

    #region HM,LL,P initialization
    public class ValueAndCost
    {
        public float value { get; set; }
        public int cost { get; set; }
        public ValueAndCost(float v, int c)
        {
            value = v;
            cost = c;
        }
    }
    private static readonly Dictionary<int, Dictionary<int, ValueAndCost>> HomingMissileData =
    new Dictionary<int, Dictionary<int, ValueAndCost>>
    {
        {
            1,
            new Dictionary<int, ValueAndCost>
            {
                {0, new ValueAndCost(5,200)},
                {1, new ValueAndCost(10,600)},
                {2, new ValueAndCost(20,1000)},
                {3, new ValueAndCost(30,1000)},
                {4, new ValueAndCost(0,0)}

            }
        },
        {
            2,
            new Dictionary<int, ValueAndCost>
            {
                {0, new ValueAndCost(5,300)},
                {1, new ValueAndCost(10,800)},
                {2, new ValueAndCost(20,1300)},
                {3, new ValueAndCost(30,1300)},
                {4, new ValueAndCost(0,0)}

            }
        },
        {
            3,
            new Dictionary<int, ValueAndCost>
            {
                {0, new ValueAndCost(5,700)},
                {1, new ValueAndCost(10,1000)},
                {2, new ValueAndCost(20,40)},//G
                {3, new ValueAndCost(30,40)},//G
                {4, new ValueAndCost(0,0)}

            }
        }
    };

    private static readonly Dictionary<int, Dictionary<int, ValueAndCost>> LongLaserData =
    new Dictionary<int, Dictionary<int, ValueAndCost>>
    {
        {
            1,
            new Dictionary<int, ValueAndCost>
            {
                {0, new ValueAndCost(5,200)},
                {1, new ValueAndCost(10,600)},
                {2, new ValueAndCost(20,1000)},
                {3, new ValueAndCost(30,1000)},
                {4, new ValueAndCost(0,0)}

            }
        },
        {
            2,
            new Dictionary<int, ValueAndCost>
            {
                {0, new ValueAndCost(10,400)},
                {1, new ValueAndCost(10,800)},
                {2, new ValueAndCost(20,1200)},
                {3, new ValueAndCost(30,1200)},
                {4, new ValueAndCost(0,0)}

            }
        },
        {
            3,
            new Dictionary<int, ValueAndCost>
            {
                {0, new ValueAndCost(5,400)},
                {1, new ValueAndCost(10,600)},
                {2, new ValueAndCost(20,40)},//G
                {3, new ValueAndCost(30,40)},//G
                {4, new ValueAndCost(0,0)}

            }
        }
    };

    private static readonly Dictionary<int, Dictionary<int, ValueAndCost>> ProtectionShieldData =
    new Dictionary<int, Dictionary<int, ValueAndCost>>
    {
        {
            1,
            new Dictionary<int, ValueAndCost>
            {
                {0, new ValueAndCost(5,200)},
                {1, new ValueAndCost(7,350)},
                {2, new ValueAndCost(9,600)},
                {3, new ValueAndCost(12,600)},
                {4, new ValueAndCost(0,0)}

            }
        },
        {
            2,
            new Dictionary<int, ValueAndCost>
            {
                {0, new ValueAndCost(5,200)},
                {1, new ValueAndCost(7,350)},
                {2, new ValueAndCost(9,600)},
                {3, new ValueAndCost(12,600)},
                {4, new ValueAndCost(0,0)}

            }
        },
        {
            3,
            new Dictionary<int, ValueAndCost>
            {
                {0, new ValueAndCost(5,200)},
                {1, new ValueAndCost(7,350)},
                {2, new ValueAndCost(9,600)},
                {3, new ValueAndCost(12,600)},
                {4, new ValueAndCost(0,0)}

            }
        }
    };

    #endregion

    //gets the homing missile cost value according to their levels..
    public static int GetPropsCostData(int level, string tag)
    {
        if (tag == "Ship1HM")
            return (HomingMissileData[1])[level].cost;
        else if (tag == "Ship2HM")
            return (HomingMissileData[2])[level].cost;
        else if (tag == "Ship3HM")
            return (HomingMissileData[3])[level].cost;

        else if (tag == "Ship1Laser")
            return (LongLaserData[1])[level].cost;
        else if (tag == "Ship2Laser")
            return (LongLaserData[2])[level].cost;
        else if (tag == "Ship3Laser")
            return (LongLaserData[3])[level].cost;

        else if (tag == "Ship1Protection")
            return (ProtectionShieldData[1])[level].cost;
        else if (tag == "Ship2Protection")
            return (ProtectionShieldData[2])[level].cost;
        else if (tag == "Ship3Protection")
            return (ProtectionShieldData[3])[level].cost;

        else
            return 0;

    }
    //gets the homing missile damage value according to their levels..
    public static float GetPropsPowerData(int level, string tag)
    {
        if (tag == "Ship1HM")
            return (HomingMissileData[1])[level].value;
        else if (tag == "Ship2HM")
            return (HomingMissileData[2])[level].value;
        else if (tag == "Ship3HM")
            return (HomingMissileData[3])[level].value;

        else if (tag == "Ship1Laser")
            return (LongLaserData[1])[level].value;
        else if (tag == "Ship2Laser")
            return (LongLaserData[2])[level].value;
        else if (tag == "Ship3Laser")
            return (LongLaserData[3])[level].value;

        else if (tag == "Ship1Protection")
            return (ProtectionShieldData[1])[level].value;
        else if (tag == "Ship2Protection")
            return (ProtectionShieldData[2])[level].value;
        else if (tag == "Ship3Protection")
            return (ProtectionShieldData[3])[level].value;

        else
            return 0;
    }





}
