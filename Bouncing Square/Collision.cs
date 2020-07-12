using GXPEngine; // For Vec2, GameObject

public class Collision {
	public readonly Vec2 normal;
	public readonly GameObject other;
	public readonly float timeOfImpact;

	public Collision(Vec2 pNormal, GameObject pOther, float pTimeOfImpact) {
		normal = pNormal;
		other = pOther;
		timeOfImpact = pTimeOfImpact;
	}
}
