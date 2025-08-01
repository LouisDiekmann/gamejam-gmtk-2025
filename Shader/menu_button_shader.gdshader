shader_type canvas_item;


uniform int levels = 3;
uniform float spread = 0.01;

void vertex() {
	VERTEX = vec2(VERTEX.x, VERTEX.y + sin(TIME));
}

void fragment(){
}
