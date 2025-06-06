    using UnityEngine;

    public class Cvoffee : Coffees
    {
        // 정신력 +5 + 장애물 1회 무시
        public override void Init()
        {
            UpMental = 5f;
            coffeeType = coffeeTypes.CvCoffee;
        }

        public override void SpecFunction()
        {
<<<<<<<< HEAD:Assets/KMJ/Scripts/Coffee/CvCoffee.cs
            GameManager.Instance.protection = true;
        }
========
        
            GameManager.Instance.protection = true;
            Debug.Log($"프로텍션 획득! 현재 {GameManager.Instance.protection}");
    }
>>>>>>>> origin/Ddddddddddngzn2:Assets/Scripts_ddngzn2/Coffee_DD2/CvCoffee.cs
    }
