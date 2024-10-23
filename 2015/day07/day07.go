package day07

import (
	"embed"
	"strconv"
	"strings"
)

//go:embed input.txt
var content embed.FS

// Run day five
func Run() (int, int) {
	input, _ := content.ReadFile("input.txt")
	lines := strings.Split(string(input), "\n")
	wires := map[string]uint16{}

	for ok := true; ok; ok = len(lines) > 0 {
		popped := lines[0]
		lines = lines[1:]
		if !executeCommand(popped, wires) {
			lines = append(lines, popped)
		}
	}

	return int(wires["a"]), 0
}

func executeCommand(line string, w map[string]uint16) bool {
	line = strings.ReplaceAll(line, "-> ", "")

	if strings.Contains(line, "AND") {
		line = strings.ReplaceAll(line, "AND ", "")
		parts := strings.Split(line, " ")

		first, ok0 := w[parts[0]]
		second, ok1 := w[parts[1]]

		if !ok0 || !ok1 {
			return false
		}

		result := first & second

		destination := parts[2]
		w[destination] = result

		return true
	}

	if strings.Contains(line, "OR") {
		line = strings.ReplaceAll(line, "OR ", "")
		parts := strings.Split(line, " ")

		first, ok0 := w[parts[0]]
		second, ok1 := w[parts[1]]

		if !ok0 || !ok1 {
			return false
		}

		result := first | second

		destination := parts[2]
		w[destination] = result

		return true
	}

	if strings.Contains(line, "LSHIFT") {
		line = strings.ReplaceAll(line, "LSHIFT ", "")
		parts := strings.Split(line, " ")

		source, ok := w[parts[0]]

		if !ok {
			return false
		}

		steps, _ := strconv.Atoi(parts[1])

		result := source << steps

		destination := parts[2]
		w[destination] = result

		return true
	}

	if strings.Contains(line, "RSHIFT") {
		line = strings.ReplaceAll(line, "RSHIFT ", "")
		parts := strings.Split(line, " ")

		source, ok := w[parts[0]]

		if !ok {
			return false
		}

		steps, _ := strconv.Atoi(parts[1])

		result := source >> steps

		destination := parts[2]
		w[destination] = result

		return true
	}

	if strings.Contains(line, "NOT") {
		line = strings.ReplaceAll(line, "NOT ", "")
		parts := strings.Split(line, " ")

		source, ok := w[parts[0]]

		if !ok {
			return false
		}
		result := ^source

		destination := parts[1]
		w[destination] = result

		return true
	}

	parts := strings.Split(line, " ")
	val, _ := strconv.ParseUint(parts[0], 10, 16)

	w[parts[1]] = uint16(val)

	return true
}
