#ifndef _SIMENGINE_H
	#define _SIMENGINE_H
#endif

using namespace std;

class WorldBase;

class EGroupManager{};
class Entity{};
class EntityManager{};
class Player{};
class __declspec(dllimport) PlayerManager
{
public:
	void AddPlayer(Player*);
};
class Scar{};
class __declspec(dllimport) SimManager
{
public:
	Scar* __thiscall GetScar();
	SimManager(float, WorldBase*);
};
class SGroupManager{};
class Squad{};
class SquadManager{};
class TerrainManager{};
class __declspec(dllimport) World
{
public:
	EntityManager* __thiscall GetEntityManager();
	PlayerManager* __thiscall GetPlayerManager();
	SimManager* __thiscall GetSimManager();
	SquadManager* __thiscall GetSquadManager();
	TerrainManager* __thiscall GetTerrainManager();
};
class WorldBase{};
__declspec(dllimport) World* g_World;