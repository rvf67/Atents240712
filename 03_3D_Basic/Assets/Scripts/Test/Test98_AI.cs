using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.InputSystem;
using UnityEngine.AI;
public class Test98_AI : TestBase
{
    public NavMeshSurface surface;
    public NavMeshAgent agent;
    AsyncOperation navAsync;
    

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        StartCoroutine(UpdateSurface());
    }

    //protected override void OnTestLClick(InputAction.CallbackContext context)
    //{

    //    Ray ray = Camera.main.ScreenPointToRay(screen);
    //    if(Physics.Raycast(ray, out RaycastHit hit,1000 , LayerMask.GetMask("Ground")))
    //    {
    //        agent.SetDestination(hit.point);
    //    }
    //}

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        //agent.remainingDistance; //목적지 까지 남아있는 거리
        //agent.pathPending; //길찾기 계산이 끝난는지 아닌지 알려루는 프로퍼티(계산중에는 true)
    }
    IEnumerator UpdateSurface()
    {
        navAsync = surface.UpdateNavMesh(surface.navMeshData);
        while (!navAsync.isDone)//비동기 명령이 끝날 때까지 반복
        {
            yield return null; //한 프레임 대기
        }
        Debug.Log("NavMesh 업데이트 완료");
    }
}
