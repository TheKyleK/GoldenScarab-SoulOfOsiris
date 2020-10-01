using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatDecorator : Decorator
{
    float time;
    float interval;
    int times;
    int repeats;
    public RepeatDecorator(float time, float interval, int repeats)
    {
        this.time = time;
        this.interval = interval;
        times = 0;
        this.repeats = repeats;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        time += dt;
        if (times < repeats && time >= interval)
        {
            time -= interval;
            times++;
            return child.Execute(agent, blackboard, interval);
        }
        return BehaviourResult.Failure;
    }
}
