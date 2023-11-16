using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using System.Threading;

public class AsyncTest : MonoBehaviour
{
	void Start()
	{
		Debug.Log("Run() invoked in Start()");
		Run(10);
		Debug.Log("Run() returns");
	}

	void Update()
	{
		Debug.Log("Update()");
	}

	async void Run(int count)
	{
		// 새로 만들어진 태스크 스레드에서 CountAsync()를 실행한다.
		var task = Task.Run(() => CountAsync(10));

		// 함수를 리턴하고 태스크가 종료될 때까지 기다린다.
		// 따라서 바로 "Run() returns" 로그가 출력된다.
		// 태스크가 끝나면 result 에는 CountAsync() 함수의 리턴값이 저장된다.
		int result = await task;

		// 태스크가 끝나면 await 바로 다음 줄로 돌아와서 나머지가 실행되고 함수가 종료된다.
		Debug.Log("Result : " + result);
	}

	int CountAsync(int count)
	{
		int result = 0;

		for (int i = 0; i < count; ++i)
		{
			Debug.Log(i);
			result += i;
			Thread.Sleep(1000);
		}

		return result;
	}
}
