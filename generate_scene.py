#!/usr/bin/env python3
"""Deterministic scene template generator for Godot 4."""
import os

def generate_main_tscn():
    """Generate a minimal playable main.tscn with Player, Ground, and Camera."""
    return """[gd_scene load_steps=3 format=3]

[ext_resource type="Script" path="res://player.gd" id="1"]
[ext_resource type="Script" path="res://ground.gd" id="2"]

[node name="Main" type="Node2D"]

[node name="Player" type="CharacterBody2D" parent="."]
script = ExtResource("1")

[node name="Sprite2D" type="Sprite2D" parent="Player"]

[node name="CollisionShape2D" type="CollisionShape2D" parent="Player"]
shape = RectangleShape2D.new()
shape.size = Vector2(32, 32)

[node name="Ground" type="StaticBody2D" parent="."]
script = ExtResource("2")

[node name="Sprite2D" type="Sprite2D" parent="Ground"]

[node name="CollisionShape2D" type="CollisionShape2D" parent="Ground"]
shape = RectangleShape2D.new()
shape.size = Vector2(256, 32)

[node name="Camera2D" type="Camera2D" parent="."]
"""

if __name__ == "__main__":
    with open("main.tscn", "w", encoding="utf-8") as f:
        f.write(generate_main_tscn())
    print("Generated main.tscn successfully.")