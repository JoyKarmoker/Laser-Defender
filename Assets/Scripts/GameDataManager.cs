using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataManager
{
    #region Shop

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
                        {2, new ValueAndCost(25,1000)},
                        {3, new ValueAndCost(50,1000)},
                        {4, new ValueAndCost(50,0)}

                    }
                },
                {
                    2,
                    new Dictionary<int, ValueAndCost>
                    {
                        {0, new ValueAndCost(8,300)},
                        {1, new ValueAndCost(28,800)},
                        {2, new ValueAndCost(60,1300)},
                        {3, new ValueAndCost(92,1300)},
                        {4, new ValueAndCost(92,0)}

                    }
                },
                {
                    3,
                    new Dictionary<int, ValueAndCost>
                    {
                        {0, new ValueAndCost(18,700)},
                        {1, new ValueAndCost(43,1000)},
                        {2, new ValueAndCost(73,1200)},
                        {3, new ValueAndCost(103,1200)},
                        {4, new ValueAndCost(103,0)}

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
                        {2, new ValueAndCost(25,1000)},
                        {3, new ValueAndCost(50,1000)},
                        {4, new ValueAndCost(50,0)}

                    }
                },
                {
                    2,
                    new Dictionary<int, ValueAndCost>
                    {
                        {0, new ValueAndCost(10,400)},
                        {1, new ValueAndCost(20,800)},
                        {2, new ValueAndCost(50,1200)},
                        {3, new ValueAndCost(100,1200)},
                        {4, new ValueAndCost(100,0)}

                    }
                },
                {
                    3,
                    new Dictionary<int, ValueAndCost>
                    {
                        {0, new ValueAndCost(12,500)},
                        {1, new ValueAndCost(32,800)},
                        {2, new ValueAndCost(62,1200)},
                        {3, new ValueAndCost(105,1250)},
                        {4, new ValueAndCost(105,0)}

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
                        {0, new ValueAndCost(4,200)},
                        {1, new ValueAndCost(5,350)},
                        {2, new ValueAndCost(6.5f,600)},
                        {3, new ValueAndCost(8.5f,600)},
                        {4, new ValueAndCost(8.5f,0)}

                    }
                },
                {
                    2,
                    new Dictionary<int, ValueAndCost>
                    {
                        {0, new ValueAndCost(4,200)},
                        {1, new ValueAndCost(5,350)},
                        {2, new ValueAndCost(6.5f,600)},
                        {3, new ValueAndCost(8.5f,600)},
                        {4, new ValueAndCost(8.5f,0)}

                    }
                },
                {
                    3,
                    new Dictionary<int, ValueAndCost>
                    {
                        {0, new ValueAndCost(4,200)},
                        {1, new ValueAndCost(5,350)},
                        {2, new ValueAndCost(6.5f,600)},
                        {3, new ValueAndCost(8.5f,600)},
                        {4, new ValueAndCost(8.5f,0)}

                    }
                }
            };

    #endregion

    //gets the homing missile cost value according to their levels..
    public static int GetPropsCostData(int level, string tag)
    {
        if (tag == AllStringConstants.HOMINGMISSILE_SHIP1)
            return (HomingMissileData[1])[level].cost;
        else if (tag == AllStringConstants.HOMINGMISSILE_SHIP2)
            return (HomingMissileData[2])[level].cost;
        else if (tag == AllStringConstants.HOMINGMISSILE_SHIP3)
            return (HomingMissileData[3])[level].cost;

        else if (tag == AllStringConstants.lASER_SHIP1)
            return (LongLaserData[1])[level].cost;
        else if (tag == AllStringConstants.lASER_SHIP2)
            return (LongLaserData[2])[level].cost;
        else if (tag == AllStringConstants.lASER_SHIP3)
            return (LongLaserData[3])[level].cost;

        else if (tag == AllStringConstants.PROTECTION_SHIP1)
            return (ProtectionShieldData[1])[level].cost;
        else if (tag == AllStringConstants.PROTECTION_SHIP2)
            return (ProtectionShieldData[2])[level].cost;
        else if (tag == AllStringConstants.PROTECTION_SHIP3)
            return (ProtectionShieldData[3])[level].cost;

        else
            return 0;

    }
    //gets the homing missile damage value according to their levels..
    public static float GetPropsPowerData(int level, string tag)
    {
        if (tag == AllStringConstants.HOMINGMISSILE_SHIP1)
            return (HomingMissileData[1])[level].value;
        else if (tag == AllStringConstants.HOMINGMISSILE_SHIP2)
            return (HomingMissileData[2])[level].value;
        else if (tag == AllStringConstants.HOMINGMISSILE_SHIP3)
            return (HomingMissileData[3])[level].value;

        else if (tag == AllStringConstants.lASER_SHIP1)
            return (LongLaserData[1])[level].value;
        else if (tag == AllStringConstants.lASER_SHIP2)
            return (LongLaserData[2])[level].value;
        else if (tag == AllStringConstants.lASER_SHIP3)
            return (LongLaserData[3])[level].value;

        else if (tag == AllStringConstants.PROTECTION_SHIP1)
            return (ProtectionShieldData[1])[level].value;
        else if (tag == AllStringConstants.PROTECTION_SHIP2)
            return (ProtectionShieldData[2])[level].value;
        else if (tag == AllStringConstants.PROTECTION_SHIP3)
            return (ProtectionShieldData[3])[level].value;

        else
            return 0;
    }

    #endregion

    #region Player



    #endregion

    #region Enemy



    #endregion

}
