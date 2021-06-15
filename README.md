# Diamon Square Terrain Generation in Unity

This is a C# implementation of the diamond square algorithm for terrain generation in Unity.

## How to use

* Add new emplty object to scene
* Add Object Spawner script to newly added object
* Add Terrain Mat from the Graphics folder
* Play around with the sliders

## Slider meaning

### Density
Defines the density of the terrain mesh. It grows exponentially so there are few options (denser mesh usually produces better results)

### Size
Defines the size of the terrain field.

### Offset
Defines the range of the random off set when the algorithm moves through the vertices. Visualy this defines how rough the terrain will be.

### Random offset
Defines the base offset for when the algorithm moves through the vertices. Visualy this defines how big the difference is between the highest and lowest point.

## Examples

![image](https://user-images.githubusercontent.com/48961914/122132573-5c306580-ce3b-11eb-816e-2176e4908257.png)

![image](https://user-images.githubusercontent.com/48961914/122132643-7ff3ab80-ce3b-11eb-8178-1733876e1f42.png)

![image](https://user-images.githubusercontent.com/48961914/122132685-9b5eb680-ce3b-11eb-81aa-393a6636a1ea.png)
