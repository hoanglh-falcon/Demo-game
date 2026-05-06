extends Label

var score_left = 0
var score_right = 0

func add_point(side):
	if side == "left":
		score_left += 1
	else:
		score_right += 1
	text = str(score_left) + " - " + str(score_right)