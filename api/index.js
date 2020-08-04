const server = require('./server/server')
require('./conn/database')
require('./server/router')(server)