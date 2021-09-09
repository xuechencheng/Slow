//***************************************************************************************
// GameTimer.h by Frank Luna (C) 2011 All Rights Reserved.
//***************************************************************************************

#ifndef GAMETIMER_H
#define GAMETIMER_H

class GameTimer
{
public:
	GameTimer();
	/// <summary>
	/// 运行总时长
	/// </summary>
	/// <returns></returns>
	float TotalTime()const;
	/// <summary>
	/// 两次Tick间隔秒数
	/// </summary>
	/// <returns></returns>
	float DeltaTime()const; 
	/// <summary>
	/// Reset程序开始时候调用一次
	/// </summary>
	void Reset(); 
	/// <summary>
	/// 开始，停止之后也可以再开始
	/// </summary>
	void Start(); 
	/// <summary>
	/// 停止，Stop
	/// </summary>
	void Stop(); 
	/// <summary>
	/// 每一帧的tick
	/// </summary>
	void Tick();  

private:
	/// <summary>
	/// 每秒多少次Count
	/// </summary>
	double mSecondsPerCount;
	/// <summary>
	/// 两次Tick间隔秒数
	/// </summary>
	double mDeltaTime;
	/// <summary>
	/// Reset的时刻计数
	/// </summary>
	__int64 mBaseTime;
	/// <summary>
	/// 暂停总时长
	/// </summary>
	__int64 mPausedTime;

	__int64 mStopTime;
	/// <summary>
	/// 上一次tick的时刻计数
	/// </summary>
	__int64 mPrevTime;
	/// <summary>
	/// 这一次tick的时刻计数
	/// </summary>
	__int64 mCurrTime;
	/// <summary>
	/// 是否停止
	/// </summary>
	bool mStopped;
};

#endif // GAMETIMER_H