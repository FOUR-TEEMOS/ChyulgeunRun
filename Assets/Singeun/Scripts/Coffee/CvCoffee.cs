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
            GameManager.Instance.protection = true;
        }
    }
