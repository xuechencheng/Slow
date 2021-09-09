//***************************************************************************************
// GameTimer.cpp by Frank Luna (C) 2011 All Rights Reserved.
//***************************************************************************************

#include <windows.h>
#include "GameTimer.h"

GameTimer::GameTimer()
: mSecondsPerCount(0.0), mDeltaTime(-1.0), mBaseTime(0), 
  mPausedTime(0), mPrevTime(0), mCurrTime(0), mStopped(false)
{
	__int64 countsPerSec;
	QueryPerformanceFrequency((LARGE_INTEGER*)&countsPerSec);
	mSecondsPerCount = 1.0 / (double)countsPerSec;
}
/// <summary>
/// 运行总时长
/// </summary>
/// <returns></returns>
float GameTimer::TotalTime()const
{
	if( mStopped )
	{
		return (float)(((mStopTime - mPausedTime) - mBaseTime) * mSecondsPerCount);
	}
	else
	{
		return (float)(((mCurrTime - mPausedTime) - mBaseTime) * mSecondsPerCount);
	}
}
/// <summary>
/// 两次Tick间隔秒数
/// </summary>
/// <returns></returns>
float GameTimer::DeltaTime()const
{
	return (float)mDeltaTime;
}
/// <summary>
/// Reset程序开始时候调用一次
/// </summary>
void GameTimer::Reset()
{
	__int64 currTime;
	QueryPerformanceCounter((LARGE_INTEGER*)&currTime);
	mBaseTime = currTime;
	mPrevTime = currTime;
	mStopTime = 0;
	mStopped  = false;
}
/// <summary>
/// 开始，停止之后也可以再开始
/// </summary>
void GameTimer::Start()
{
	__int64 startTime;
	QueryPerformanceCounter((LARGE_INTEGER*)&startTime);
	if( mStopped )
	{
		mPausedTime += (startTime - mStopTime);	
		mPrevTime = startTime;
		mStopTime = 0;
		mStopped  = false;
	}
}
/// <summary>
/// 停止，Stop
/// </summary>
void GameTimer::Stop()
{
	if( !mStopped )
	{
		__int64 currTime;
		QueryPerformanceCounter((LARGE_INTEGER*)&currTime);
		mStopTime = currTime;
		mStopped  = true;
	}
}
/// <summary>
/// 每一帧的tick
/// </summary>
void GameTimer::Tick()
{
	if( mStopped )
	{
		mDeltaTime = 0.0;
		return;
	}
	__int64 currTime;
	QueryPerformanceCounter((LARGE_INTEGER*)&currTime);
	mCurrTime = currTime;
	mDeltaTime = (mCurrTime - mPrevTime) * mSecondsPerCount;
	mPrevTime = mCurrTime;
	if(mDeltaTime < 0.0)
	{
		mDeltaTime = 0.0;
	}
}

