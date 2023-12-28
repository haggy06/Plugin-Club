using System.Collections.Generic;
using UnityEngine;

internal static class YieldInstructionCache // 데이터 저장용이라 MonoBehaviour가 필요없음. 대신 internal로 싱글톤 비슷한 효과를 냄.
{
    //public static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame(); // 여러 형식을 저장할 수 있단 의미로 작성한 코드. 이 프로젝트에서는 쓰지 않음.

    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();
    //                          └ cpp의 Map<key, value> 과 같은구조임.
    public static WaitForSeconds WaitForSeconds(float second)
    {
        WaitForSeconds wfs;

        if (!waitForSeconds.TryGetValue(second, out wfs)) // Dictionary에 일치하는 키 값이 있지 않을 경우(있으면 (out wfs)으로 Dictionary 안의 값을 리턴함.)
        {
            waitForSeconds.Add(second, wfs = new WaitForSeconds(second)); // 입력된 키 값으로 새 객체를 만들어 Dictionary에 저장함.
        }
        return wfs;
    }
}
