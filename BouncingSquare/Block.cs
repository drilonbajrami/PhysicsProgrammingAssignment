using System;
using GXPEngine;


public class Block : EasyDraw
{
	/******* PUBLIC FIELDS AND PROPERTIES *********************************************************/

	// These four public static fields are changed from MyGame, based on key input (see Console):
	public static bool drawDebugLine = false;
	public static bool wordy = false;
	public static float bounciness = 0.98f;
	// For ease of testing / changing, we assume every block has the same acceleration (gravity):
	public static Vec2 acceleration = new Vec2 (0, 0);

	public readonly int radius;

	// Mass = density * volume.
	// In 2D, we assume volume = area (=all objects are assumed to have the same "depth")
	public float Mass {
		get {
			return 4 * radius * radius * _density;
		}
	}

	public Vec2 position {
		get {
			return _position;
		}
	}

	public Vec2 velocity;

	/******* PRIVATE FIELDS *******************************************************************/

	Vec2 _position;
	Vec2 _oldPosition;

	Arrow _velocityIndicator;

	float _red = 1;
	float _green = 1;
	float _blue = 1;

	float _density = 1;

	const float _colorFadeSpeed = 0.025f;

	/******* PUBLIC METHODS *******************************************************************/

	public Block (int pRadius, Vec2 pPosition, Vec2 pVelocity) : base (pRadius*2, pRadius*2)
	{
		radius = pRadius;
		_position = pPosition;
		velocity = pVelocity;

		SetOrigin (radius, radius);
		draw ();
		UpdateScreenPosition();
		_oldPosition = new Vec2(0,0);

		_velocityIndicator = new Arrow(_position,velocity, 10);
		AddChild(_velocityIndicator);
	}

	public void SetFadeColor(float pRed, float pGreen, float pBlue) {
		_red = pRed;
		_green = pGreen;
		_blue = pBlue;
	}

	public void Update() {
		// For extra testing flexibility, we call the Step method from MyGame instead:
		//Step();
	}

	public void Step() {
		_oldPosition=_position;

		// No need to make changes in this Step method (most of it is related to drawing, color and debug info). 
		// Work in Move instead.
		Move();

		UpdateColor();
		UpdateScreenPosition();
		ShowDebugInfo();
	}

	/******* PRIVATE METHODS *******************************************************************/

	void Move() {
        // TODO: implement Assignment 3 here (and in methods called from here).
        velocity.Add(acceleration);
        _position.Add(velocity);

		// Example methods (replace/extend):
		//CheckBoundaryCollisions();
		//CheckBlockOverlaps();

        // TIP: You can use the Collision class to pass information between methods, e.g.:
        //
        Collision firstCollision = FindEarliestCollision();
        if (firstCollision != null)
            ResolveCollision(firstCollision);
    }

    void ResolveCollision(Collision col)
    {
        if(col.normal.x != 0)
        {
            _position = _oldPosition + velocity * col.timeOfImpact;
            velocity.x *= -bounciness;
        } else
        {
            _position = _oldPosition + velocity * col.timeOfImpact;
            velocity.y *= -bounciness;
        }
    }

    Collision FindEarliestCollision()
    {
        Collision earliestCollision = null;
        MyGame myGame = (MyGame)game;
        if (_position.x - radius < myGame.LeftXBoundary)
        {
            float toi = TimeOfImpact(_oldPosition.x, _position.x, myGame.LeftXBoundary + radius);
            if (earliestCollision == null || toi < earliestCollision.timeOfImpact)
            {
                earliestCollision = new Collision(new Vec2(1, 0), null, toi);
            }
            SetFadeColor(1, 0.2f, 0.2f);
            if (wordy)
            {
                Console.WriteLine("Left boundary collision");
            }
        }
        else if (_position.x + radius > myGame.RightXBoundary)
        {
            float toi = TimeOfImpact(_oldPosition.x, _position.x, myGame.RightXBoundary - radius);
            if (earliestCollision == null || toi < earliestCollision.timeOfImpact)
            {
                earliestCollision = new Collision(new Vec2(-1, 0), null, toi);
            }
            SetFadeColor(1, 0.2f, 0.2f);
            if (wordy)
            {
                Console.WriteLine("Right boundary collision");
            }
        }
        if (_position.y - radius < myGame.TopYBoundary)
        {
            float toi = TimeOfImpact(_oldPosition.y, _position.y, myGame.TopYBoundary + radius);
            if (earliestCollision == null || toi < earliestCollision.timeOfImpact)
            {
                earliestCollision = new Collision(new Vec2(0, 1), null, toi);
            }
            SetFadeColor(0.2f, 1, 0.2f);
            if (wordy)
            {
                Console.WriteLine("Top boundary collision");
            }
        }
        else if (_position.y + radius > myGame.BottomYBoundary)
        {
            float toi = TimeOfImpact(_oldPosition.y, _position.y, myGame.BottomYBoundary - radius);
            if (earliestCollision == null || toi < earliestCollision.timeOfImpact)
            {
                earliestCollision = new Collision(new Vec2(0, -1), null, toi);
            }
            SetFadeColor(0.2f, 1, 0.2f);
            if (wordy)
            {
                Console.WriteLine("Bottom boundary collision");
            }
        }
        return earliestCollision;
    }
    // Time Of Impact
    public float TimeOfImpact(float startPoint, float endPoint, float hitPoint)
    {
        return (hitPoint - startPoint) / (endPoint - startPoint);
    }

    // This method is just an example of how to check boundaries, and change color.
    void CheckBoundaryCollisions() {
		MyGame myGame = (MyGame)game;
		if (_position.x - radius < myGame.LeftXBoundary) {
			// move block from left to right boundary:
			_position.x += myGame.RightXBoundary - myGame.LeftXBoundary - 2 * radius;
			SetFadeColor(1, 0.2f, 0.2f);
			if (wordy) {
				Console.WriteLine ("Left boundary collision");
			}
		} else if (_position.x + radius > myGame.RightXBoundary) {
			// move block from right to left boundary:
			_position.x -= myGame.RightXBoundary - myGame.LeftXBoundary - 2 * radius;
			SetFadeColor(1, 0.2f, 0.2f);
			if (wordy) {
				Console.WriteLine ("Right boundary collision");
			}
		}
		if (_position.y - radius < myGame.TopYBoundary) {
			// move block from top to bottom boundary:
			_position.y += myGame.BottomYBoundary - myGame.TopYBoundary - 2 * radius;
			SetFadeColor(0.2f, 1, 0.2f);
			if (wordy) {
				Console.WriteLine ("Top boundary collision");
			}
		} else if (_position.y + radius > myGame.BottomYBoundary) {
			// move block from bottom to top boundary:
			_position.y -= myGame.BottomYBoundary - myGame.TopYBoundary - 2 * radius;
			SetFadeColor(0.2f, 1, 0.2f);
			if (wordy) {
				Console.WriteLine ("Bottom boundary collision");
			}
		}
	}

	// This method is just an example of how to get information about other blocks in the scene.
	void CheckBlockOverlaps() {
		MyGame myGame = (MyGame)game;
		for (int i = 0; i < myGame.GetNumberOfMovers(); i++) {
			Block other = myGame.GetMover(i);
			if (other != this) {
				// TODO: improve hit test, move to method:
				if (Mathf.Abs(other.position.x - _position.x) < 25 &&
					Mathf.Abs(other.position.y - _position.y) < 25) {
					SetFadeColor(0.2f, 0.2f, 1);
					other.SetFadeColor(0.2f, 0.2f, 1);
					if (wordy) {
						Console.WriteLine ("Block-block overlap detected.");
					}
				}
			}
		}
	}

	void UpdateColor() {
		if (_red < 1) {
			_red = Mathf.Min (1, _red + _colorFadeSpeed);
		}
		if (_green < 1) {
			_green = Mathf.Min (1, _green + _colorFadeSpeed);
		}
		if (_blue < 1) {
			_blue = Mathf.Min (1, _blue + _colorFadeSpeed);
		}
		SetColor(_red, _green, _blue);
	}

	void ShowDebugInfo() {
		if (drawDebugLine) {
			((MyGame)game).DrawLine (_oldPosition, _position);
		}
		_velocityIndicator.startPoint = _position;
		_velocityIndicator.vector = velocity;
	}

	void UpdateScreenPosition() {
		x = _position.x;
		y = _position.y;
	}

	void draw() {
		Fill (200);
		NoStroke ();
		ShapeAlign (CenterMode.Min, CenterMode.Min);
		Rect (0, 0, width, height);
	}
}
