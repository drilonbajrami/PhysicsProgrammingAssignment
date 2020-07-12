using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{	

	static void Main() {
		new MyGame().Start();
	}

	Ball _ball;
	EasyDraw _text;
	NLineSegment _lineSegment;
    float _bounciness = Utils.Random(0.0f, 1.0f);

	public MyGame () : base(800, 600, false,false)
	{
		_ball = new Ball (30, new Vec2 (width / 2 - 80, height / 6));
		AddChild (_ball);

        _text = new EasyDraw(250, 25);
        _text.TextAlign(CenterMode.Min, CenterMode.Min);
        AddChild(_text);

        _lineSegment = new NLineSegment (new Vec2 (500, 500), new Vec2 (100, 200), 0xff00ff00, 3);
		AddChild(_lineSegment);
	}

	void Update () {
		// For now: this just puts the ball at the mouse position:
		_ball.Step ();

        //TODO: calculate correct distance from ball center to line
        Vec2 distanceLineBall = _lineSegment.end - _ball.position;
        Vec2 lineVector = _lineSegment.start - _lineSegment.end;
		float ballDistance = distanceLineBall.Dot(lineVector.Normal());   //HINT: it's NOT 10000

		//compare distance with ball radius
		if (ballDistance < _ball.radius)
        {
            _ball.position -= (-ballDistance + _ball.radius) * lineVector.Normal();
            _ball.velocity.ReflectOnLine(lineVector, _bounciness);
            _ball.SetColor (1, 0, 0);
		} else {
			_ball.SetColor (0, 1, 0);
		}

        _text.Clear(Color.Transparent);
        _text.Text("Distance to line: " + ballDistance, 0, 0);
        Reset();
	}

    void Reset()
    {
        if (Input.GetKeyDown(Key.R))
        {
            _ball.Destroy();
            RemoveChild(_ball);
            _lineSegment.Destroy();
            RemoveChild(_lineSegment);
            _ball = new Ball(30, new Vec2(width / 2 - 80, height / 6));
            AddChild(_ball);
            _lineSegment = new NLineSegment(new Vec2(500, 500), new Vec2(100, 200), 0xff00ff00, 3);
            AddChild(_lineSegment);
            _bounciness = Utils.Random(0.0f, 1.0f);
        }
    }
}

