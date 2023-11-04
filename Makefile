doc-serve:
	docfx ./docs/docfx.json --serve

doc-build:
	docfx ./docs/docfx.json

.PHONY: doc-serve doc-build