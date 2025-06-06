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
            Debug.Log($"프로텍션 획득! 현재 {GameManager.Instance.protection}");
        }
    }
