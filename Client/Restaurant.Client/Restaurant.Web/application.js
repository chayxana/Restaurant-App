var React = require('react');

var food = React.createClass({displayName: "food",

	render: function() {
		return (
			React.createElement("div", null, 
				"Hello World!"
			)
		);
	}

});

module.exports = food;
var React = require('react');

var order = React.createClass({displayName: "order",

	render: function() {
		return (
			React.createElement("div", {className: ""}
				
			)		
		);
	}

});

module.exports = order;