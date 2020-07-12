using GXPEngine;

class Bullet : Sprite 
{
	// public fields & properties:
	public Vec2 position 
	{
		get 
		{
			return _position;
		}
	}
	public Vec2 velocity;

	// private fields:
	Vec2 _position;

	public Bullet(Vec2 pPosition, Vec2 pVelocity, float pRotation) : base("assets/bullet.png") 
	{
		_position = pPosition;
		velocity = pVelocity;
        rotation = pRotation;
	}

	void UpdateScreenPosition() 
	{
		x = _position.x;
		y = _position.y;
	}

	public void Update() 
	{
		_position.Add (velocity);
		UpdateScreenPosition ();
	}
}
