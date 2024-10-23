package day01

import (
	"embed"
)

//go:embed input.txt
var content embed.FS

// Run day one
func Run() (int, int) {
	input, _ := content.ReadFile("input.txt")
	strInput := string(input)

	part1 := RunElevator(strInput)
	part2 := FindBasement(strInput)

	return part1, part2
}

func FindBasement(input string) int {
	floor := 0
	pos := 0
	for _, ch := range input {
		if ch == '(' {
			floor++
		} else {
			floor--
		}

		pos++
		if floor < 0 {
			return pos
		}
	}

	return floor
}

func RunElevator(input string) int {
	floor := 0
	for _, ch := range input {
		if ch == '(' {
			floor++
		} else {
			floor--
		}
	}

	return floor
}
