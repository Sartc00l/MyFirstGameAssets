using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
   public Transform enemy;
   public void CreateEnemey()
    {
        for (int i = 0; i > 2; i++)
        {
            Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
